set "docfx=%~dp0..\packages\docfx.console.2.57.2\tools\docfx.exe"
set "json=%~dp0docfx.json"

%docfx% init -q
start "" "http://localhost:8080/"
%docfx% %json% --serve