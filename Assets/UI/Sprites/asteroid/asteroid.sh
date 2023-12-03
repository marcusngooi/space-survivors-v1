# Jasper den Ouden,  6-9-12
# License: CC0, or public domain.(whichever)
#
#Inspired on: http://i-simplicity.de/tutorials.html#asteroid
#
#Generates asteroids.

#NOTE: this is a dumb shell script. If it stops working, ah-well.
#Useage:
# export variables or define them right before the call.
# Just pitch
#
#Examples:
# sh asteroid.sh
# size=0.5 sh asteroid.sh
# lz = -800 sh asteroid.lsh
#
#Variables:
#
# w,h:       width and height. (800,800)
#
# size:      size of asteroid (0.5 to 0.7 ok) (0.5)
# x,y,z:     camera position. (17,0,0)
# angle:     angle of view. (10)
#            (has to be tuned with size and x,y,z)
#
# lx,ly,lz:  light source position (500,500,800)
# to_pov:    .pov file to output. (/tmp/`date +%s`.pov)
# to_png:    .png file to output. (/output/`date +%s`.png)
# 
# version:   povray version. (3.7)

random()
{ echo $RANDOM
}

at_time=`date +%s`

version=${version-3.7}
x=${x-17}
y=${y-0}
z=${z-0}

angle=${angle-10}

lx=${lx-500}
ly=${ly-500}
lz=${lz-800}

size=${size-0.5}

to_pov=${to_pov-/tmp/current_asteroid.pov}
to_png=${to_png-output/$at_time.png}

w=${w-800}
h=${h-800}

echo "//Autogenerated from dir `pwd`
#version $version; 

#include \"colors.inc\"
#include \"functions.inc\"

global_settings
{ assumed_gamma 1.0
  radiosity {
    pretrace_start 0.08
    pretrace_end   0.01
    count 150
    nearest_count 10
    error_bound 0.5
    recursion_limit 1
    low_error_factor 0.5
    gray_threshold 0.0
    minimum_reuse 0.005
    maximum_reuse 0.2
    brightness 1
    adc_bailout 0.005
  } 
}

camera{
  location <$x,$y,$z>
  look_at 0
  angle $angle
}
light_source{ <$lx, $ly, $lz> White }

#declare BASE_SHAPE=
function
{
  sqrt(x*x+y*y+z*z) - 1
}

#declare CRATER_SHAPE_TEMPLT=
function
{
  pigment
  {
    crackle form <1.5,0,0>
    color_map
    {
      [0 rgb    <1.0,1.0,1.0>]
      [0.75 rgb <0.0,0.0,0.0>]
      [1 rgb    <0.2,0.2,0.2>]
    }
    cubic_wave
  }
}

#declare smoothe_crater = function(p,xo)
{ min(p*p/(xo*(2-xo)), (2*(p-xo)+xo)/(2-xo)) }

#declare CRATER_SHAPE=
function(x,y,z,S)
{
  smoothe_crater(CRATER_SHAPE_TEMPLT(x/S,y/S,z/S).red, 0.1) * 
  (1 - 0.04*sqrt(f_noise3d(50*(x+`random`),50*(y+`random`),50*(z+`random`)))/
            (0.04+pow(CRATER_SHAPE_TEMPLT(x/S,y/S,z/S).red,2)))
}

isosurface
{
  function
    {
      BASE_SHAPE(x,y,z) - $size
      + 0.5 * f_noise3d(x/10+`random`,y/10+`random`,z/10+`random`)
      + 0.2 * f_noise3d(x+`random`,y+`random`,z+`random`)
      + 0.2 * f_noise3d(x+`random`,y+`random`,z+`random`)
      + 0.2 * pow(f_noise3d(2*x,2*y,2*z),2)
      + 0.01* f_noise3d(20*(x+`random`),20*(y+`random`),20*(z+`random`))
      + .04 * CRATER_SHAPE(x+`random`,y+`random`,z+`random`,.35) *
        (1 + f_noise3d(x+`random`,y+`random`,z+`random`))
      + .01 * CRATER_SHAPE(x+`random`,y+`random`,z+`random`,.1)
    }
  contained_by{box{-1.2 1.2}}
  threshold 0

  max_gradient 2.0

  texture
  {
    pigment
    {
      bozo
      color_map
      {
        [0 rgb <0.3,0.3,0.3>]
        [1 rgb <1.0,1.0,1.0>]
      }
      scale 0.2
    }
    finish
    {
      ambient 0.0
      diffuse 1.0
      brilliance 1.0
      specular 0.1
      roughness 0.08
    }
  }
  normal
  {
    agate 0.13
    scale 0.08
  }
}" > $to_pov

povray -I$to_pov -O$to_png +Q11 +A0.5 +W$w +H$h +UA

echo Rendered $to_png

echo "at_time $at_time
version=$version
x=$x
y=$y
z=$z
angle=$angle
lx=$lx
ly=$ly
lz=$lz
size=$size
to_pov=$to_pov
to_png=$to_png
w=$w
h=$h" > $to_png.info