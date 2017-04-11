# FtpDownloader

<img src="https://i.gyazo.com/5ea90169ac5150e0868205087cae0daa.png" align="center" />

FtpDownloader is an Windows Forms application that lets you download an entire FTP(Linux or Windows based) just by entering its path and credentials.

[VirusTotal](https://www.virustotal.com/en/file/343624ae7e7b28adc9cfece8f9e5d1e8a45988246307e967973f7d1b84da251a/analysis/1491870988/)

## Introduction

As the title show, this application basically reads the introduced FTP path with its username and password, and then starts to download from there to every single subdirectory found.

The way it works is pretty simple, it makes a call to check and retrieve a list of current files/directorys of a certain ftp path, then downloads all the files, and then creates a folder. When the folder is created, it checks again for files and directories, this will do it until there is no more directories inside directories.

## Important

1. The credentials must have enough rights to download from the FTP server.
2. The download path has to have FULL rights.

## Questions and issues

You can use the github issue tracker for bug reports, feature requests, questions.

## License

MIT License

Copyright (c) 2017 Emiliano Montesdeoca del Puerto

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
