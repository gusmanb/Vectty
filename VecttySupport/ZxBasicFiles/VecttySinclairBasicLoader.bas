9000 REM VECTTY DRAWING ROUTINE. ADD (10 CLEAR 59904 : LOAD "" CODE 59904) AT THE BEGINNING OF YOUR CODE. RESTORE THE IMAGE DATA'S (EX. RESTORE 8000) AND EXECUTE GO SUB 9000 TO DRAW IT
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
9680 PRINT AT y0, x0; " "
9690 RETURN
9700 IF att>23295 THEN RETURN
9710 READ cr
9720 IF att+1<23296 THEN GO TO 9760
9730 POKE att,cr
9740 LET att=att+1
9750 GO TO 9700
9760 READ ds
9770 IF cr=ds THEN GO TO 9820
9780 POKE att,cr
9790 LET att=att+1
9800 LET cr=ds
9810 GO TO 9720
9820 READ ds
9830 IF ds = 0 THEN GO TO 9920
9840 POKE 60019, ds
9850 LET op = INT(att / 256)
9860 POKE 60016, att - (op * 256)
9870 POKE 60017, op
9880 POKE 60018, cr
9890 RANDOMIZE USR 60020
9900 LET att = att + ds + 2
9910 GO TO 9700
9920 POKE att, cr
9930 LET att = att + 1
9940 POKE att, cr
9950 LET att = att + 1
9960 GO TO 9700