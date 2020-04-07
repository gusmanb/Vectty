using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vectty.ZXClasses
{
    public static class RenderFunctions
    {
        public const string SinclairBasicFunction = @"9000 REM VECTTY DRAWING ROUTINE. ADD (10 CLEAR 59904 : LOAD """" CODE 59904) AT THE BEGINNING OF YOUR CODE. RESTORE THE IMAGE DATA'S (EX. RESTORE 8000) AND EXECUTE GO SUB 9000 TO DRAW IT
9001 LET att=22528
9002 LET op=0
9003 LET x0=0
9004 LET y0=0
9005 LET y1=0
9006 LET ds=0
9007 LET cr=0
9010 READ cr
9020 IF cr=255 THEN GO TO 9700
9030 IF cr<200 THEN GO TO 9070
9040 LET op=cr
9050 READ y0
9060 GO TO 9080
9070 LET y0=cr
9080 IF op=200 OR op=250 THEN GO SUB 9160
9090 IF op=201 THEN GO SUB 9310
9100 IF op=202 THEN GO SUB 9400
9110 IF op=203 THEN GO SUB 9450
9120 IF op=204 THEN GO SUB 9620
9130 IF op=205 THEN GO SUB 9670
9150 GO TO 9010
9160 READ x0
9170 READ y1
9180 READ x1
9190 PLOT x0,y0
9200 DRAW x1-x0,y1-y0
9210 IF op<>250 THEN RETURN
9220 LET x0=x1
9230 LET y0=y1
9240 READ cr
9250 IF cr=200 THEN GO TO 9160
9260 IF cr>200 THEN LET op=cr: READ y0: RETURN
9270 LET y1=cr
9280 READ x1
9290 DRAW x1-x0,y1-y0
9300 GO TO 9220
9310 READ x0
9320 READ y1
9330 READ x1
9340 PLOT x0,y0
9350 DRAW x1-x0,0
9360 DRAW 0,y1-y0
9370 DRAW x0-x1,0
9380 DRAW 0,y0-y1
9390 RETURN
9400 READ x0
9410 READ ds
9420 CIRCLE x0,y0,ds
9430 RETURN
9450 READ x0
9460 READ y1
9470 READ x1
9480 READ ds
9490 LET ds=ds*256
9500 READ cr
9510 LET ds=ds+cr
9520 IF ds>=32768 THEN GO TO 9560
9530 LET ds=ds/10.0
9540 LET ds=ds*-1
9550 GO TO 9580
9560 LET ds=ds-32768
9570 LET ds=ds/10.0
9580 LET ds=(PI/180.0)*ds
9590 PLOT x0,y0
9600 DRAW x1-x0,y1-y0,ds
9610 RETURN
9620 READ x0
9630 POKE 59904,x0
9640 POKE 59905,y0
9650 RANDOMIZE USR 59906
9660 RETURN
9670 READ x0
9680 PRINT AT y0, x0; "" ""
9690 RETURN
9700 IF att>23295 THEN RETURN
9710 READ cr
9720 IF att+1<23296 THEN GO TO 9760
9730 POKE att, cr
9740 LET att = att + 1
9750 GO TO 9700
9760 READ ds
9770 IF cr = ds THEN GO TO 9820
9780 POKE att, cr
9790 LET att = att + 1
9800 LET cr = ds
9810 GO TO 9720
9820 READ ds
9830 IF ds = 0 THEN GO TO 9920
9840 POKE 60019, ds
9850 LET op = INT(att / 256)
9860 POKE 60016, att - (op* 256)
9870 POKE 60017, op
9880 POKE 60018, cr
9890 RANDOMIZE USR 60020
9900 LET att = att + ds + 2
9910 GO TO 9700
9920 POKE att, cr
9930 LET att = att + 1
9940 POKE att, cr
9950 LET att = att + 1
9960 GO TO 9700";

        public const string ZXBasicFunction = @"#define ATTRSTART 22528
#define ATTREND 23296
#define LINETOOL 200
#define RECTTOOL 201
#define CIRCLETOOL 202
#define ARCTOOL 203
#define FILLTOOL 204
#define ERASETOOL 205
#define POLYTOOL 250
#define ENDOPS 255

sub Dibujar(ImageSource as uinteger)

    dim imgAddr as uinteger = ImageSource
    dim attrAddr as uInteger = ATTRSTART
    Dim current as uByte
    Dim nxt as uByte
    Dim runLen as uInteger
    Dim runLenPrev as uInteger

    'Recreamos las operaciones de dibujo
    
    dim op as ubyte
    dim x0 as integer
    dim y0 as integer
    dim x1 as integer
    dim y1 as integer
    dim dist as uinteger

    dim deg as float

    while op <> ENDOPS

        current = peek(imgAddr)
        
        if current > 191 then 
            op = current
            imgAddr = imgAddr + 1
        end if

        if op = LINETOOL or op = POLYTOOL then

            y0 = peek(imgAddr)
            x0 = peek(imgAddr + 1)
            y1 = peek(imgAddr + 2)
            x1 = peek(imgAddr + 3)
            imgAddr = imgAddr + 4

            plot x0, y0
            draw x1 - x0, y1 - y0

            if op = POLYTOOL then

                while y1 < 200

                    x0 = x1
                    y0 = y1

                    y1 = peek(imgAddr)

                    if y1 < 200 then

                        x1 = peek(imgAddr + 1)
                        imgAddr = imgAddr + 2

                        draw x1 - x0, y1 - y0

                    end if                                            

                end while

            end if

        else if op = RECTTOOL then
    
            y0 = peek(imgAddr)
            x0 = peek(imgAddr + 1)
            y1 = peek(imgAddr + 2)
            x1 = peek(imgAddr + 3)
            imgAddr = imgAddr + 4

            plot x0, y0
            draw x1 - x0, 0
            draw 0, y1 - y0
            draw x0 - x1, 0
            draw 0, y0 - y1

        else if op = CIRCLETOOL then

            y0 = peek(imgAddr)
            x0 = peek(imgAddr + 1)
            dist = peek(imgAddr + 2)
            imgAddr = imgAddr + 3

            circle x0, y0, dist

        else if op = ARCTOOL then

            y0 = peek(imgAddr)
            x0 = peek(imgAddr + 1)
            y1 = peek(imgAddr + 2)
            x1 = peek(imgAddr + 3)

            dist = peek(imgAddr + 4)
            dist = dist << 8
            dist = dist bor peek(imgAddr + 5)

            imgAddr = imgAddr + 6

            if dist >= 32768 then
                deg = dist - 32768
                deg = deg / 10.0
            else
                deg = dist
                deg = deg / 10.0
                deg = deg * -1
            end if

            deg = (PI / 180.0) * deg

            plot x0, y0
            draw x1 - x0, y1 - y0, deg

        else if op = FILLTOOL then

			y0 = peek(imgAddr)
            x0 = peek(imgAddr + 1)
			imgAddr = imgAddr + 2

            FloodFill(x0, y0)

        else if op = ERASETOOL

			y0 = peek(imgAddr)
            x0 = peek(imgAddr + 1)
			imgAddr = imgAddr + 2

			print at y0, x0 ; "" "";

        end if

    end while

    while attrAddr<ATTREND

        current = PEEK(imgAddr)

        if attrAddr + 1 < ATTREND then

            nxt = PEEK(imgAddr + 1)

            if nxt = current then

                runLen = PEEK(imgAddr + 2)
                runLen = runLen + 2

                runLenPrev = runLen

                while runLen > 0

                    POKE attrAddr, current
                    runLen = runLen - 1
                    attrAddr = attrAddr + 1

                end while

                imgAddr = imgAddr + 3

            else

                POKE attrAddr, current
                attrAddr = attrAddr + 1
                imgAddr = imgAddr + 1

            end if

        else

            POKE attrAddr, current
            attrAddr = attrAddr + 1
            imgAddr = imgAddr + 1

        end if

    end while

end sub

sub fastcall FloodFill(x as ubyte, y as ubyte)

asm

ld d, a
pop hl
pop bc
ld e, b
push hl 

fill:
  ld l,255
  push hl

nextrun:
  ld a, d
  and 7
  inc a
  ld b, a
  ld a,1
bitpos:
  rrca
  djnz bitpos
  ld c,b
  ld b,a

seekleft:
  ld a, d
  or a
  jr z, goright
  dec d
  rlc b
  call scrpos
  jr nz, seekleft

seekright:  
  rrc b
  inc d
  jr z, rightedge
goright:
  call scrpos
  jr z, rightedge
  ld(hl),a
 inc e
 call checkadj
 dec e
 dec e
 call checkadj
 inc e
 jr seekright

rightedge:
  pop de
  ld a, e
  inc a
  jr nz, nextrun
  ret  

scrpos:
  ld a, e
  and 248
  rra
  scf
  rra
  rra
  ld l, a
  xor e
  and 248
  xor e
  ld h, a
  ld a, l
  xor d
  and 7
  xor d
  rrca
  rrca
  rrca
  ld l,a
  ld a,b
  or(hl)
  cp(hl)
  ret


checkadj:
  sla c
  ld a, e
  cp 192
  ret nc
  call scrpos+1
  ret z
  inc c
  bit 2,c
  ret nz
  pop hl
  push de
  jp(hl)


end asm

end sub";

    }
}
