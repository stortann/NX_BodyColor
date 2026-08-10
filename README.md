# NX_BodyColor
## Funcionality
In a part, this journal:<br />
1/ Turns off Translucency<br />
2/ Changes session's settings to Part Shininess<br />
3/ Turns off Shaded Edges<br />
4/ Sets Background to Plain White (1.0, 1.0, 1.0)<br />
5/ Changes all of the lights to 0.0 except "Ambient" which will be 1.0<br />
6/ Hides known things that are not a body - this code needs to be updated in case that it didn't hide something that you need<br />
7/ Changes colors of bodies according to a specific rule: the color of a body will not be changed except for bodies that have one of the following colors:<br />
{186, 6, 211, 31} = red, yellow, blue, cyan <br />
in which case the body will be given color from the array below, from the index where this color was in the first array:<br />
{1, 159, 201, 210} = white, medium gray, iron gray, charcoal grey

For example, if the body was colored in the 211, then it will be repainted to the 201.
