ASFLAGS=-O0 -s --64 --gdwarf-2
LDFLAGS=-static

.PHONY: all clean exec


all:
        make chxid

chxid:
        as $(ASFLAGS) chxid_asm.s -o chxid.o
        ld $(LDFLAGS) chxid.o -o Chxid

clean:
        rm -f *.o Chxid

exec:
        chmod +x Chxid
