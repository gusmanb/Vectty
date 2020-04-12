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

        public const string ZXBasicFunction = @"


'Funciones de dibujado

#define ATTRSTART 22528
#define ATTREND 23296

#define LINETOOL 200
#define RECTTOOL 201
#define CIRCLETOOL 202
#define ARCTOOL 203
#define FILLTOOL 204
#define TEXTURETOOL 205
#define POLYTOOL 250
#define ENDOPS 255

Dim ptrnSolido(7) as uByte => { $FF,$FF,$FF,$FF,$FF,$FF,$FF,$FF }

sub Dibujar(ImageSource as uinteger, PatternSource as uInteger)

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

            if op = POLYTOOL then 'Si es una poli linea

                while y1 < 200 'Mientras no haya un cambio de herramienta

                    x0 = x1 'Guardamos las ultimas coordenadas
                    y0 = y1

                    y1 = peek(imgAddr) 'Leemos el siguiente byte

                    if y1 < 200 then 'Si sigue siendo la poli linea...

                        x1 = peek(imgAddr + 1) 'Acabamos de leer las coordenadas
                        imgAddr = imgAddr + 2  'Actualizamos la direccion

                        draw x1 - x0, y1 - y0  'Dibujamos el nuevo segmento

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

            SPPFill (x0, y0, @ptrnSolido(0))

        else if op = TEXTURETOOL

			y0 = peek(imgAddr)
            x0 = peek(imgAddr + 1)
			dist = peek(imgAddr + 2)

			imgAddr = imgAddr + 3

			SPPFill (x0, y0, PatternSource + (dist * 8))

        end if

    end while



    'Descomprimimos los atributos
    while attrAddr < ATTREND

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

sub fastcall SPPFill (xCoord as uByte, yCoord as uByte, fillPatternAddress as uInteger)
	
	asm
	
	; Patterned Flood Fill
	; Alvin Albrecht 2002
	; Tweaked to work in ZX Basic by Britlion, 2012.
	; Tweaked to respect IX by El Dr. Gusman, 2012
	;
	; This subroutine does a byte-at-a-time breadth-first patterned flood fill.
	; It works by allocating a circular queue on the stack, with the size of the
	; queue determined by an input parameter.  The queue is divided into three
	; contiguous blocks: the pattern block, the investigate block and the new block.
	; The queue contains 3-byte structures (described below) with a special structure
	; delimiting each block.  The physical end of the queue is marked with a special
	; byte.  The contents of the queue grow down in memory.
	;
	; The pattern block holds pixels that have been blackened on screen and are
	; only waiting to have the pattern applied to them before they are removed
	; from the queue.
	;
	; The investigate block holds pixels that have been blackened on screen and
	; are waiting to be investigated before they become part of the pattern
	; block.  'Investigating' a pixel means trying to expand the fill in all
	; four directions.  Any successful expansion causes the new pixel to be
	; added to the new block.
	;
	; The new block holds pixels that have been blackened on screen and are
	; waiting to become part of the investigate block.  The new block expands
	; as the investigate block is investigated.
	;
	; The pattern fill algorithm follows these steps:
	; 1. Examine the investigate block.  Add new pixels to the new block if an
	;    expansion is possible.
	; 2. Pattern the pattern block.  All pixels waiting to be patterned are
	;    patterned on screen.
	; 3. The investigate block becomes the pattern block.
	;    The new block becomes the investigate block.
	; 4. Repeat until the investigate block is empty.
	;
	; The algorithm may bail out prematurely if the queue fills up.  Bailing
	; out causes any pending pixels in the queue to be patterned before the
	; subroutine exits.  If PFILL would continue to run by refusing to enter
	; new pixels when the queue is full, there would be no guarantee that
	; the subroutine would ever return.
	;
	; In English, the idea behind the patterned flood fill is simple.  The
	; start pixel grows out in a circular shape (actually a diamond).  A
	; fill boundary two pixels thick is maintained in that circular shape.
	; The outermost boundary is the frontier, and is where the flood fill
	; is growing from (ie the investigate block).  The inner boundary is
	; the pattern block, waiting to be patterned.  A solid inner boundary
	; is necessary to prevent the flood-fill frontier pixels from growing
	; back toward the starting pixel through holes in the pattern.  Once
	; the frontier pixels are investigated, the innermost boundary is
	; patterned.  The newly investigated pixels become the outermost
	; boundary (the investigate block) and the old investigate block becomes
	; the new pattern block.
	;
	; Each entry in the queue is a 3-byte struct that grows down in memory:
	;       screen address      (2-bytes, MSB first)
	;       fill byte           (1-byte)
	; A screen address with MSB < 0x40 is used to indicate the end of a block.
	; A screen address with MSB >= 0x80 is used to mark the physical end of the Q.
	;
	; The fill pattern is a typical 8x8 pixel character, stored in 8 bytes.
	
	; enter: h = y coord, l = x coord, bc = max stack depth, de = address of fill pattern
	;        In hi-res mode, carry flag is most significant bit of x coord
	; used : ix, af, bc, de, hl
	; exit : no carry = success, carry = had to bail queue was too small
	; stack: 3*bc+30 bytes, not including the call to PFILL or interrupts

	JP SPPFill_start 
	
	SPGetScrnAddr:
	   and $07
	   or $40
	   ld d,a
	   ld a,h
	   rra
	   rra
	   rra
	   and $18
	   or d
	   ld d,a
	
	   ld a,l
	   and $07
	   ld b,a
	   ld a,$80
	   jr z, norotate
	
	rotloop:
	   rra
	   djnz rotloop
	
	norotate:
	   ld b,a
	   srl l
	   srl l
	   srl l
	   ld a,h
	   rla
	   rla
	   and $e0
	   or l
	   ld e,a
	   ret
	
	SPPixelUp:
	   ld a,h
	   dec h
	   and $07
	   ret nz
	   ld a,$08
	   add a,h
	   ld h,a
	   ld a,l
	   sub $20
	   ld l,a
	   ret nc
	   ld a,h
	   sub $08
	   ld h,a
	   cp $40
	   ret

	SPPixelDown:
	   inc h
	   ld a,h
	   and $07
	   ret nz
	   ld a,h
	   sub $08
	   ld h,a
	   ld a,l
	   add a,$20
	   ld l,a
	   ret nc
	   ld a,h
	   add a,$08
	   ld h,a
	   cp $58
	   ccf
	   ret

	SPCharLeft:
	   ld a,l
	   dec l
	   or a
	   ret nz
	   ld a,h
	   sub $08
	   ld h,a
	   cp $40
	   ret
	
	SPCharRight:
	   inc l
	   ret nz
	   ld a,8
	   add a,h
	   ld h,a
	   cp $58
	   ccf
	   ret
	
	SPPFill_start:
	POP BC ; return address
	POP HL ; Gets Y coord into H
	POP DE ; Gets Fill pattern into DE
	LD L,A ; Gets Y,X into HL
	PUSH BC ; Puts return address back
	
	PUSH IX ; preserve IX

	LD BC,300 ; Bytes allowed in stack.
	 
	 
	; enter: h = y coord, l = x coord, bc = max stack depth, de = address of fill pattern
	;        In hi-res mode, carry flag is most significant bit of x coord
	
	
	SPPFill:
	   push de			; save (pattern pointer) variable
	   dec bc			; we will start with one struct in the queue
	   push bc			; save max stack depth variable
	
	   ld a,h
	   call SPGetScrnAddr	; de = screen address, b = pixel byte
	   ex de,hl			; hl = screen address
	   call bytefill		; b = fill byte
	   jr c, viable
	   pop bc
	   pop de
	   jp SPPFill_end ; quit - not viable.
	
	viable:
	   ex de,hl			; de = screen address, b = fill byte
	   ld hl,-7
	   add hl,sp
	   push hl			; create pattern block pointer = top of queue
	   push hl
	   pop ix			; ix = top of queue
	   dec hl
	   dec hl
	   dec hl
	   push hl			; create investigate block pointer
	   ld hl,-12
	   add hl,sp
	   push hl			; create new block pointer
	
	   xor a
	   push af
	   dec sp			; mark end of pattern block
	   push de			; screen address and fill byte are
	   push bc			;   first struct in investigate block
	   inc sp
	   push af
	   dec sp			; mark end of investigate block
	
	   ld c,(ix+7)
	   ld b,(ix+8)		; bc = max stack depth - 1
	   inc bc
	   ld l,c
	   ld h,b
	   add hl,bc		; space required = 3*BC (max depth) + 10
	   add hl,bc		; but have already taken 9 bytes
	   ld c,l
	   ld b,h			; bc = # uninitialized bytes in queue
	   ld hl,0
	   sbc hl,bc		; negate hl, additions above will not set carry
	   add hl,sp
	   ld (hl),0		; zero last byte in queue
	   ld sp,hl			; move stack below queue
	   ld a,$80
	   push af			; mark end of queue with $80 byte
	   inc sp
	   ld e,l
	   ld d,h
	   inc de
	   dec bc
	   ldir			; zero the uninitialized bytes in queue
	   
	; NOTE: Must move the stack before clearing the queue, otherwise if an interrupt
	; occurred, garbage could overwrite portions of the (just cleared) queue.
	
	; ix = top of queue, bottom of queue marked with 0x80 byte
	
	; Variables indexed by ix, LSB first:
	;   ix + 11/12    return address
	;   ix + 09/10    fill pattern pointer
	;   ix + 07/08    max stack depth
	;   ix + 05/06    pattern block pointer
	;   ix + 03/04    investigate block pointer
	;   ix + 01/02    new block pointer
	
	; A picture of memory at this point:
	;
	;+-----------------------+   higher addresses
	;|                       |         |
	;|-   return address    -|        \|/
	;|                       |         V
	;+-----------------------+   lower addresses
	;|        fill           |
	;|-  pattern pointer    -|
	;|                       |
	;+-----------------------+
	;|                       |
	;|-  max stack depth    -|
	;|                       |
	;+-----------------------+
	;|                       |
	;|-   pattern block     -|
	;|                       |
	;+-----------------------+
	;|                       |
	;|- investigate block   -|
	;|                       |
	;+-----------------------+
	;|                       |
	;|-     new block       -|
	;|                       |
	;+-----------------------+
	;|  end of block marker  |  <- ix = pattern block = top of queue
	;|          ?            |
	;|          ?            |
	;+-----------------------+
	;|  screen address MSB   |  <- investigate block
	;|  screen address LSB   |
	;|      fill byte        |
	;+-----------------------+
	;|  end of block marker  |
	;|          ?            |
	;|          ?            |
	;+-----------------------+
	;|          0            |  <- new block
	;|          0            |
	;|          0            |
	;+-----------------------+
	;|                       |
	;|        ......         |  size is a multiple of 3 bytes
	;|     rest of queue     |
	;|      all zeroed       |
	;|        ......         |
	;|                       |
	;+-----------------------+
	;|         0x80           |  <- sp, special byte marks end of queue
	;+-----------------------+
	
	pfloop:
	   ld l,(ix+3)
	   ld h,(ix+4)		; hl = investigate block
	   ld e,(ix+1)
	   ld d,(ix+2)		; de = new block
	   call investigate
	   ld (ix+1),e
	   ld (ix+2),d		; save new block
	   ld (ix+3),l
	   ld (ix+4),h		; save investigate block
	
	   ld l,(ix+5)
	   ld h,(ix+6)		; hl = pattern block
	   ld c,(ix+7)
	   ld b,(ix+8)		; bc = max stack depth (available space)
	   call applypattern
	   ld (ix+7),c
	   ld (ix+8),b		; save stack depth
	   ld (ix+5),l
	   ld (ix+6),h		; save pattern block
	
	   ld a,(hl)		; done if the investigate block was empty
	   cp 40h
	   jp nc, pfloop
	
	endpfill:
	   ld de,11			; return address is at ix+11
	   add ix,de
	   ld sp,ix
	   or a			; make sure carry is clear, indicating success
	   pop ix
	   ret
	
	; IN/OUT: hl = investigate block, de = new block
	
	investigate:
	   ld a,(hl)		
	   cp 80h			; bit 15 of screen addr set if time to wrap		
	   jp c, inowrap
	   push ix
	   pop hl			; hl = ix = top of queue
	   ld a,(hl)
	
	inowrap:
	   cp 40h			; screen address < 0x4000 marks end of block
	   jp c, endinv		; are we done yet?
	   ld b,a
	   dec hl
	   ld c,(hl)		; bc = screen address
	   dec hl
	   ld a,(hl)		; a = fill byte
	   dec hl
	   push hl			; save spot in investigate block
	   ld l,c
	   ld h,b			; hl = screen address
	   ld b,a			; b = fill byte
	   
	goup:
	   push hl			; save screen address
	   call SPPixelUp		; move screen address up one pixel
	   jr c, updeadend		; if went off-screen
	   push bc			; save fill byte
	   call bytefill
	   call c, addnew		; if up is not dead end, add this to new block
	   pop bc			; restore fill byte
	
	updeadend:
	   pop hl			; restore screen address
	   
	godown:
	   push hl			; save screen address
	   call SPPixelDown		; move screen address down one pixel
	   jr c, downdeadend
	   push bc			; save fill byte
	   call bytefill
	   call c, addnew		; if down is not dead end, add this to new block
	   pop bc			; restore fill byte
	
	downdeadend:
	   pop hl			; restore screen address
	
	goleft:
	   bit 7,b			; can only move left if leftmost bit of fill byte set
	   jr z, goright
	   ld a,l
	   and 31
	   jr nz, okleft
	   bit 5,h              ; for hi-res mode: column = 1 if l=0 and bit 5 of h is set
	   jr z, goright
	
	okleft:
	   push hl			; save screen address
	   call SPCharLeft
	   push bc			; save fill byte
	   ld b,01h		; set rightmost pixel for incoming byte
	   call bytefill
	   call c, addnew		; if left is not dead end, add this to new block
	   pop bc			; restore fill byte
	   pop hl			; restore screen address
	
	goright:
	   bit 0,b			; can only move right if rightmost bit of fill byte set
	   jr z, nextinv
	   or a			; clear carry
	   call SPCharRight
	   jr c, nextinv     	; went off screen
	   ld a,l
	   and 31
	   jr z, nextinv  	; wrapped around line
	   ld b,80h		; set leftmost pixel for incoming byte
	   call bytefill
	   call c, addnew		; if right is not dead end, add this to new block
	
	nextinv:
	   pop hl			; hl = spot in investigate block
	   jp investigate
	
	endinv:
	   dec hl
	   dec hl
	   dec hl			; investigate block now points at new block
	
	   ld a,(de)		; check if new block is at end of queue
	   cp 80h
	   jr c, nowrapnew
	   defb $dd
	   ld e,l
	   defb $dd
	   ld d,h			; de = ix = top of queue
	
	nowrapnew:
	   xor a
	   ld (de),a		; store end marker for new block
	   dec de
	   dec de
	   dec de
	   ret
	
	; enter b = incoming byte, hl = screen address
	; exit  b = fill byte, screen blackened with fill byte
	
	bytefill:
	   ld a,b
	   xor (hl)			; zero out incoming pixels that
	   and b			; run into set pixels in display
	   ret z
	
	bfloop:
	   ld b,a
	   rra			; expand incoming pixels
	   ld c,a			; to the right and left
	   ld a,b			; within byte
	   add a,a
	   or c
	   or b
	   ld c,a
	   xor (hl)			; zero out pixels that run into
	   and c			; set pixels on display
	   cp b
	   jp nz, bfloop		; keep going until incoming byte does not change
	
	   or (hl)
	   ld (hl),a		; fill byte on screen
	   scf			; indicate that this was a viable step
	   ret
	
	; add incoming fill byte and screen address to new block
	; enter b = incoming byte, hl = screen address, de = new block
	
	addnew:
	   push hl			; save screen address
	   ld l,(ix+7)
	   ld h,(ix+8)		; hl = max stack depth
	   ld a,h
	   or l
	   jr z, bail		; no space in queue so bail!
	   dec hl			; available queue space decreases by one struct
	   ld (ix+7),l
	   ld (ix+8),h
	   pop hl			; hl = screen address
	
	   ld a,(de)		; check if new block is at end of queue
	   cp 80h
	   jr c, annowrap
	   defb $dd
	   ld e,l
	   defb $dd
	   ld d,h               ; de = ix = top of queue
	
	annowrap:
	   ex de,hl
	   ld (hl),d		; make struct, store screen address (2 bytes)
	   dec hl
	   ld (hl),e
	   dec hl
	   ld (hl),b		; store fill byte (1 byte)
	   dec hl
	   ex de,hl
	   ret
	
	; if the queue filled up, we need to bail.  Bailing means patterning any set pixels
	; which may still be on the display.  If we didnt bail and tried to trudge along,
	; there is no guarantee the fill would ever return.
	
	bail:
	   pop hl			; hl = screen address, b = fill byte
	   ld a,b
	   xor (hl)
	   ld (hl),a		; clear this byte on screen
	
	   xor a
	   ld (de),a		; mark end of new block
	
	   ld l,(ix+5)
	   ld h,(ix+6)		; hl = pattern block
	   call applypattern	; for pattern block
	   call applypattern	; for investigate block
	   call applypattern	; for new block
	
	   ld de,11			; return address is at ix+11
	   add ix,de
	   ld sp,ix
	   scf			; indicate we had to bail
	   jp SPPFill_end
	
	; hl = pattern block, bc = max stack depth (available space)
	
	applypattern:
	   ld a,(hl)
	   cp 80h			; bit 15 of screen addr set if time to wrap
	   jp c, apnowrap
	   push ix
	   pop hl			; hl = ix = top of queue
	   ld a,(hl)
	
	apnowrap:
	   cp 40h			; screen address < 0x4000 marks end of block
	   jr c, endapply		; are we done yet?
	
	   and 07h			; use scan line 0..7 to index pattern
	   add a,(ix+9)
	   ld e,a
	   ld a,0
	   adc a,(ix+10)
	   ld d,a			; de points into fill pattern
	   ld a,(de)		; a = pattern 
	
	   ld d,(hl)
	   dec hl
	   ld e,(hl)		; de = screen address
	   dec hl
	
	   and (hl)			; and pattern with fill byte
	   sub (hl)			; or in complement of fill byte
	   dec a
	   ex de,hl
	   and (hl)			; apply pattern to screen
	   ld (hl),a
	   ex de,hl
	   dec hl
	   inc bc			; increase available queue space
	   jp applypattern
	
	endapply:
	   dec hl
	   dec hl
	   dec hl			; pattern block now pts at investigate block
	   ret
	
	SPPFill_end:
	end asm

end sub";

    }
}
