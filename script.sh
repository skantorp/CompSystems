#!/bin/sh
ml icc                                                                                                                                                                  
lscpu > p
f_list=`grep "Flags" ./p | uniq | cut -d':' -f2 | cut -d" " -f2- | tr _ . | tr "a-z" "A-Z"`
        for f in $f_list
                do
                icc -O2 -x$f GnomeSort.cpp -o check.out 2> error
                        if [ ! -s "error" ]; then
                                for o in {1..3}
                                do
                                                echo "______________________________" 
                                                echo "$f -O$o compiled in"             
                                                time `icc -O$o -x$f 3ch.cpp -o run.out`
                                                echo "++++++++++++++++++++++++++++++"
                                                echo "program ran 1000 times in"
                                                time `for i in {1..1000}; do ./run.out >/dev/null; done`
                                done
                        fi
                done;