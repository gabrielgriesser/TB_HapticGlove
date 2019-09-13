<?php

function Dist2($a,$b){
 $s = ($a[0]-$b[0])**2;
 $s+= ($a[1]-$b[1])**2;
 $s+= ($a[2]-$b[2])**2;
 if(isset($a[3])) $s+= ($a[3]-$b[3])**2;
 return $s;
}

function Norm($q){
 $s = $q[0]**2 + $q[1]**2 + $q[2]**2;
 if(isset($q[3])){
  $s+=$q[3]**2;
  $s=sqrt($s);if($s==0)$s=1;
  return array($q[0]/$s,$q[1]/$s,$q[2]/$s,$q[3]/$s);
 } else
 {
  $s=sqrt($s);if($s==0)$s=1;
  return array($q[0]/$s,$q[1]/$s,$q[2]/$s);
 }
}

function Norm_Sign($q){
 $s = $q[0]**2 + $q[1]**2 + $q[2]**2;
 if(isset($q[3])){
  $s+=$q[3]**2;
  $s=sqrt($s);if($s==0)$s=1;$sign = $q[0]>0?1:(-1);
  return array($q[0]*$sign/$s,$q[1]*$sign/$s,$q[2]*$sign/$s,$q[3]*$sign/$s);
 } else
 {
  $s=sqrt($s);if($s==0)$s=1;
  return array($q[0]/$s,$q[1]/$s,$q[2]/$s);
 }
}


function Calc_W($q){
 $s = ($q[0]**2 + $q[1]**2 + $q[2]**2);
 if($s>=0.9999999){ $s=sqrt($s);$q[0]/=$s;$q[1]/=$s;$q[2]/=$s; }
 $w = sqrt(1.00000001 - ($q[0]**2 + $q[1]**2 + $q[2]**2));
 return Norm(array($w,$q[0],$q[1],$q[2]));
}

function Rotate_Quat($va,$q) {
  $qa = Norm_Sign(array(cos(($va[0]+$va[1]+$va[2])/2),sin($va[0]/2),sin($va[1]/2),sin($va[2]/2)));
  return Mult_Quat_Norm($qa,$q);
}

function Rotate_Vector($v,$q){
 $vect = array(0,$v[0],$v[1],$v[2]);
 $q_inv = array($q[0],-$q[1],-$q[2],-$q[3]);
 $a = Mult_Quat_Norm($q,$vect);
 $vect = Mult_Quat($a,$q_inv);
 return array($vect[1],$vect[2],$vect[3]);
}

function Rotate_Vector_Raw($v,$q){
 $vect = array(0,$v[0],$v[1],$v[2]);
 $q_inv = array($q[0],-$q[1],-$q[2],-$q[3]);
 $a = Mult_Quat($q,$vect);
 $vect = Mult_Quat($a,$q_inv);
 return array($vect[1],$vect[2],$vect[3]);
}

function Mult_Quat($a,$b){
 $c=$a;
 $c[0]=$a[0]*$b[0]-$a[1]*$b[1]-$a[2]*$b[2]-$a[3]*$b[3];
 $c[1]=$a[0]*$b[1]+$a[1]*$b[0]+$a[2]*$b[3]-$a[3]*$b[2];
 $c[2]=$a[0]*$b[2]-$a[1]*$b[3]+$a[2]*$b[0]+$a[3]*$b[1];
 $c[3]=$a[0]*$b[3]+$a[1]*$b[2]-$a[2]*$b[1]+$a[3]*$b[0];
 return $c;
}

function Mult_Quat_Norm($a,$b){
 $c=$a;
 $c[0]=$a[0]*$b[0]-$a[1]*$b[1]-$a[2]*$b[2]-$a[3]*$b[3];
 $c[1]=$a[0]*$b[1]+$a[1]*$b[0]+$a[2]*$b[3]-$a[3]*$b[2];
 $c[2]=$a[0]*$b[2]-$a[1]*$b[3]+$a[2]*$b[0]+$a[3]*$b[1];
 $c[3]=$a[0]*$b[3]+$a[1]*$b[2]-$a[2]*$b[1]+$a[3]*$b[0];
 if($c[0]<0){ $c[0] =- $c[0]; $c[1] =- $c[1]; $c[2] =- $c[2]; $c[3] =- $c[3]; }
 return Norm($c);
}

function detm($a, $c, $b, $d){ return $a*$d - $b*$c; }

?>