# Usage
1. Wygeneruj shellcode w pliku binarnym, a nastepnie przekształć go w base64
```
base64 -w 0 apollo.bin > apollo.base
```
2. Zaszyfruj wynikowy plik (np. apollo.base) za pomocą programu Encrypt i wskaż katalog do jakiego ma zapisać pliki wynikowe
```
.\Encrypt.exe C:\Users\piotr\Desktop\apollo.base C:\Users\piotr\Desktop\
```
3. Umieść zawartość ciperText w zmiennej cipherText
4. Umieść zawartość keyBase64 w zmiennej keyBase64
5. Umieść zawartość vectorBase64 w zmiennej vectorBase64
6. Skompiluj exeka
