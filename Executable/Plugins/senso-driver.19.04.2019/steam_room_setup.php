<?php
 $fname_out = "lh_data.ini";
 if(isset($argv[1]))$fname_out=$argv[1];

 $fname_lh = 'lighthousedb.json';
 $fname_rm = 'chaperone_info.vrchap';
 @$f  = file_get_contents($fname_lh);
 @$f1 = file_get_contents($fname_rm);

 if(!$f || !$f1){
  $path_localapp = getenv('LOCALAPPDATA');
  if(!$path_localapp){ file_put_contents($fname_out,"#Can't find SteamVR setup\n");return; }
  $vr = file_get_contents($path_localapp.'\openvr\openvrpaths.vrpath');
  if(!$vr){ file_put_contents($fname_out,"#Can't find SteamVR config\n");return; }
  @$path = json_decode($vr,TRUE);
  if(!isset($path['config'][0])){ file_put_contents($fname_out,"#SteamVR config has unknown format\n");return; }
  $vrpath=$path['config'][0];
  $fname_lh = $vrpath.'\lighthouse\lighthousedb.json';
  $fname_rm = $vrpath.'\chaperone_info.vrchap';
  @$f  = file_get_contents($fname_lh);
  @$f1 = file_get_contents($fname_rm);
 }

 file_put_contents($fname_out,"#Can't parse SteamVR configs\n");

 include "fm.php";

 if(!$f){
  file_put_contents($fname_out,"#SteamVR lighthousedb.json not found\n");return;
 }

 if(!$f1){
  file_put_contents($fname_out,"#SteamVR chaperone_info.vrchap not found\n");return;
 }


 @$o = json_decode($f,TRUE);
 if(!$o)return;

 $bases = array();
 foreach($o['base_stations'] as $name => $value){
   $serial = $value['config']['serialNumber'];
   $bases[$serial] = $value['dynamic_states']; 
 }

 $universes = array();
 foreach($o['known_universes'] as $u){
  $id = $u['id'];	
  $universes[$id]['tilt'] = $u['tilt'];
  $universes[$id]['bases'] = $u['base_stations'];
 }

 $o = json_decode($f1,TRUE);

 $universe = array();
 $ft=0;
 foreach($o['universes'] as $u){
  $ts = strtotime($u['time']);	
  if($ts>$ft){ $universe = $u;$ft = $ts; }
 }

 $id = $universe["universeID"];
 $res_u = array();

 $res_u["translation"] = $universe['standing']["translation"];
 $res_u["pitch"] = $universes[$id]['tilt']['pitch'];
 $res_u["roll"] = $universes[$id]['tilt']['roll'];
 $res_u["yaw"] = $universe['standing']['yaw'];
 $res_u["id"] = $id;
 $res_u["ts"] = $ft;

 $vt = $res_u['translation'];
 $vt = array(-1000*$vt[0],1000*$vt[2],-1000*$vt[1]);
 $vt = Rotate_Vector_Raw($vt,Rotate_Quat(array(0,0,-$res_u['yaw']),array(1,0,0,0)));

 $bs = $universes[$id]['bases'];
 $lh = array();
 foreach($bs as $bts){
  $id = $bts['base_serial_number'];
  $pose = $bts['target_pose']['pose'];
 // $did = $bts['target_pose']['dynamic_state_id'];

  $did=0;
  foreach($bases[$id] as $key => $val){
   if($bases[$id][$did]['time_last_seen'] <= $val['time_last_seen'])$did=$key; 
  }

  $bss = $bases[$id][$did];
  $bs_mode = $bss['dynamic_state']['basestation_mode'];
  $tilt = $bss['tilt'];
  $ts = $bss['time_last_seen'];
  $b = array('id'=>$id,'pose'=>$pose,'mode'=>$bs_mode,'tilt'=>$tilt,'ts'=>$ts);
  $pos = array(-1000*$pose[4],1000*$pose[6],-1000*$pose[5]);
  $quat = array($pose[3],-$pose[0],$pose[2],-$pose[1]);
//  $pos = Rotate_Vector_Raw($pos,$quat);
  $quat = Rotate_Quat(array(0,$res_u['roll'],0),$quat);
  $quat = Rotate_Quat(array(-$res_u['pitch'],0,0),$quat);
  $quat = Rotate_Quat(array(0,0,-$res_u['yaw']),$quat);
  $pos = Rotate_Vector_Raw($pos,$quat);
  $b['p']=array($pos[0] - $vt[0], $pos[1] - $vt[1], $pos[2] - $vt[2]);
  $b['q']=$quat;

  echo "got bs $id $did ts = ".$bases[$id][$did]['time_last_seen']." mode=".$bs_mode."\n";

//  if(!isset($lh[$bs_mode]) || $lh[$bs_mode]['ts']<$ts)$lh[$bs_mode] = $b;
  if(!isset($lh["$id"]) || $lh["$id"]['ts']<$ts)$lh["$id"] = $b;
 }

 //print_r($vt); 
 //print_r($res_u);
// print_r($lh);

 $new_lh = array();
 $ts=0;$last_ser='';
 foreach($lh as $ser => $bs){ if($ser!=$last_ser && $ts<$bs['ts']){ $new_lh[0]=$bs;$ts=$bs['ts']; } }
 if(count($new_lh)){
  $last_ser=$new_lh[0]['id'];
  $ts=0;
  foreach($lh as $ser => $bs){ if($ser!=$last_ser && $ts<$bs['ts']){ $new_lh[1]=$bs;$ts=$bs['ts']; } }
 }

 print_r($new_lh);

 $lh=$new_lh;
 
 ksort($lh);
 if(count($lh)<1){
  file_put_contents($fname_out,"#Can't get LH BS data from SteamVR config\n");return;
 }

 if(count($lh)>3){ 
  file_put_contents($fname_out,"#Too many LH BS in SteamVR config, it's weird\n");return;
 }

 if(count($lh)==3){
  $tss=-1;$idx=-1;
  foreach($lh as $key => $val){
   if($tss<0 || $tss>$val['ts']){ $tss = $val['ts'];$idx=$key; }
  }
  unset($lh[$idx]);
 }

 file_put_contents($fname_out,"#SteamVR room setup is correct\n");
 
 @$f = fopen($fname_out,"a");
 if(!$f){
  die("can't open output file $fname_out for writing\n");
 }
 
 foreach($lh as $cam){
  $s=sprintf("%d %f %f %f %f %f %f %f\n",$cam['mode'],$cam['p'][0],$cam['p'][1],$cam['p'][2],$cam['q'][0],$cam['q'][1],$cam['q'][2],$cam['q'][3]);
  fputs($f,$s);
  echo "BS id = ".$cam['id']." $s";
 }
 fclose($f);

 echo "File $fname_out is ready to use with senso_ui\n";

?>
