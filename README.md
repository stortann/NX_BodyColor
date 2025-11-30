# NX_BodyColor
Macro in NX so I don't have to look up the values of some colors anymore

## Funcionality
Journal desciption:<br />
1/ Changes session's settings to Part Shininess<br />
2/ Sets Background to Plain White (1.0, 1.0, 1.0)<br />
3/ Changes all of the lights to 0.0 except "Ambient" which will be 1.0<br />
4/ Turns off Shaded Edges<br />
5/ Hides everything that is not a body<br />
6/ Changes colors of bodies according to a specific rule: the color of a body will not be changed except for bodies that have one of the following colors<br />
{186, 6, 211, 31} = red, yellow, blue, cyan <br />
in that case the body will be colored according to the index from the first array in this array <br />
{1, 159, 201, 210} = white, medium gray, iron gray, charcoal grey
