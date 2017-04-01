FROM microsoft/windowsservercore
ARG source
WORKDIR /app
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["C:\app\Lab_01_08.exe"]
