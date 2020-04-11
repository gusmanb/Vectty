# Vectty
Vectorial designer for ZX Spectrum projects

## How to include in your program

### For ZX Basic (Boriel Basic)

- Select "Export" from the file toolbar
- Choose "Export as Boriel Basic"
- Check the "Include draw functions" if this is the first image you will include
- Give the container array a name (you will use it later to call the draw function)
- Specify an output file
- Accept to export it

Once it has been exported...

- Add ```#include "(yourfile.bas)```
- To draw the image call ```@Dibujar(yourImageArrayName(0))```

### For Sinclair Basic

- Select "Export" from the file toolbar
- Choose "Export as Sinclair Basic"
- Check the "Include draw functions" if this is the first image you will include (the function will be located at line 9000)
- Specify the frst line (address) of the image data
- Specify an output file
- Accept to export it

Once it has been exported...

- Add at the begining of your program: ```10 CLEAR 59904 : LOAD """" CODE 59904```
- Load the included "VecttySinclairBasicHelpers.tap" tape
- To draw the image add ```RESTORE (your image's first line number) : GO SUB 9000``` 

As the Sinclair Basic is very reduced and slow some assembler functions are needed to achieve the desired result, these functions are in the "VecttySinclairBasicHelpers.tap" tape

Sinclair Basic also can't draw on the two bottom lines, it will yield a "integer out of range" error if you try, so Vectty will not allow you to export an image in Sinclair Basic mode if any operation uses these two bottom lines
