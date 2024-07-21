#!/bin/sh

for it in *.hex; do
  objcopy -I ihex -O binary --gap-fill 0xFF "${it}" "${it%.*}.bin"
done

for it in *.HEX; do
  objcopy -I ihex -O binary --gap-fill 0xFF "${it}" "${it%.*}.bin"
done


