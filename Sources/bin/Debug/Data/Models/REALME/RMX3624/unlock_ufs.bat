spd_dump exec_addr 0x65015f08 fdl fdl1-dl.bin 0x65000800 fdl fdl2-dl.bin 0x9efffe00 exec read_part splloader 0 1m spl.bin read_part uboot_a 0 1m uboot.bin erase_part splloader erase_part splloader_bak reset
@echo "don't continue if you see find port failed, just close and re-run this batch"
pause
spd_dump exec_addr 0x65015f08 fdl fdl1-dl.bin 0x65000800 fdl fdl2-dl.bin 0x9efffe00 exec write_part uboot_a fdl2-cboot.bin write_part uboot_b fdl2-cboot.bin reset
chsize spl.bin
chsize uboot.bin
TIMEOUT /T 10 /NOBREAK
::unlock runs here, may need twice
spd_dump exec_addr 0x65015f08 fdl spl-ufs-unlock.bin 0x65000800
::check unlock (if get 64 zeros, locked; if 32 string + 16 hash + 16 hash, unlocked)
spd_dump exec_addr 0x65015f08 fdl fdl1-dl.bin 0x65000800 fdl fdl2-dl.bin 0x9efffe00 exec verbose 2 read_part miscdata 8192 64 m.bin reset
pause
::restore spl and uboot
spd_dump exec_addr 0x65015f08 fdl fdl1-dl.bin 0x65000800 fdl fdl2-dl.bin 0x9efffe00 exec write_part splloader spl.bin write_part uboot_a uboot.bin write_part uboot_b uboot.bin reset