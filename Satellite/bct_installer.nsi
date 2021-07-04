!define PRODUCT_NAME "Bulk Certification Tool"

!include MUI2.nsh

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\modern-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

;Registry keys
!define PRODUCT_DIR_REGKEY "Software\BulkCertificationTool"
!define REG_SHELL_BULKCERTIFICATIONTOOL_PATH_ALL "*\shell\BulkCertificationTool"
;!define REG_SHELL_BULKCERTIFICATIONTOOL_PATH_PDF "AcroExch.Document\shell\BulkCertificationTool"
;!define REG_SHELL_BULKCERTIFICATIONTOOL_PATH_PDF_10 "AcroExch.Document.10\shell\BulkCertificationTool"
;!define REG_SHELL_BULKCERTIFICATIONTOOL_PATH_PDF_11 "AcroExch.Document.11\shell\BulkCertificationTool"
;!define REG_SHELL_BULKCERTIFICATIONTOOL_PATH_OLD "Word.Document.8\shell\BulkCertificationTool"
;!define REG_SHELL_BULKCERTIFICATIONTOOL_PATH "Word.Document.12\shell\BulkCertificationTool"
!define REG_SHELL_BULKCERTIFICATIONTOOL_COOMANDS_BATES_PATH "BCT\Menu\Shell\bates"
!define REG_SHELL_BULKCERTIFICATIONTOOL_COOMANDS_BATES_COMMAND_PATH "BCT\Menu\Shell\bates\command"
!define REG_SHELL_BULKCERTIFICATIONTOOL_COOMANDS_MISC_PATH "BCT\Menu\Shell\misc"
!define REG_SHELL_BULKCERTIFICATIONTOOL_COOMANDS_MISC_COMMAND_PATH "BCT\Menu\Shell\misc\command"

;--------------------------------
;General

Name "${PRODUCT_NAME}"
OutFile "BulkCertificationToolSetup.exe"
RequestExecutionLevel admin
InstallDir "$PROGRAMFILES\Bulk Certification Tool"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" "Install_Dir"
ShowInstDetails show
ShowUnInstDetails show

;--------------------------------
;Pages

!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_COMPONENTS
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_WELCOME
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_UNPAGE_FINISH

; Language files
!insertmacro MUI_LANGUAGE "English"

; MUI end ------

Function .onInit
  !insertmacro MUI_LANGDLL_DISPLAY
FunctionEnd

;--------------------------------

Section "Bulk Certification Tool" MainSec
  SectionIn RO
  SetOutPath "$INSTDIR"
  SetOverwrite ifnewer
  File "..\BulkCertificationTool\bin\Release\Spire.Pdf.dll"
  File "..\BulkCertificationTool\bin\Release\Spire.License.dll"
  File "..\BulkCertificationTool\bin\Release\Spire.Doc.dll"
  File "..\BulkCertificationTool\bin\Release\ClosedXML.dll"
  File "..\BulkCertificationTool\bin\Release\DocumentFormat.OpenXml.dll"
  File "..\BulkCertificationTool\bin\Release\DocX.dll"
  File "..\BulkCertificationTool\bin\Release\BulkCertificationTool.exe"
  File "..\BulkCertificationTool\bin\Release\BulkCertificationTool.exe.config"
  
    ; Write the installation path into the registry
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "Install_Dir" "$INSTDIR"

  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\BulkCertificationTool" "DisplayName" "Bulk Certification Tool"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\BulkCertificationTool" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\BulkCertificationTool" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\BulkCertificationTool" "NoRepair" 1
  ;Enlarge more than 15 file are selected
  WriteRegDWORD HKCU "Software\Microsoft\Windows\CurrentVersion\Explorer" "MultipleInvokePromptMinimum" 0x000000FF
  ;Create uninstaller
  WriteUninstaller "$INSTDIR\Uninstall.exe"
  
  WriteRegStr HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_PATH_ALL}" "MUIVerb" "Bulk Certification Tool"
  WriteRegStr HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_PATH_ALL}" "ExtendedSubCommandsKey" "BCT\Menu"
  ;WriteRegStr HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_PATH}" "MUIVerb" "Bulk Certification Tool"
  ;WriteRegStr HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_PATH}" "ExtendedSubCommandsKey" "BCT\Menu"
  ;WriteRegStr HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_PATH_OLD}" "MUIVerb" "Bulk Certification Tool"
  ;WriteRegStr HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_PATH_OLD}" "ExtendedSubCommandsKey" "BCT\Menu"
  ;WriteRegStr HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_PATH_PDF}" "MUIVerb" "Bulk Certification Tool"
  ;WriteRegStr HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_PATH_PDF}" "ExtendedSubCommandsKey" "BCT\Menu"
  ;WriteRegStr HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_PATH_PDF_10}" "MUIVerb" "Bulk Certification Tool"
  ;WriteRegStr HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_PATH_PDF_10}" "ExtendedSubCommandsKey" "BCT\Menu"
  ;WriteRegStr HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_PATH_PDF_11}" "MUIVerb" "Bulk Certification Tool"
  ;WriteRegStr HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_PATH_PDF_11}" "ExtendedSubCommandsKey" "BCT\Menu"
  WriteRegStr HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_COOMANDS_BATES_PATH}" "MUIVerb" 'Bates numbers(single)'
  WriteRegStr HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_COOMANDS_BATES_COMMAND_PATH}" "" '"$INSTDIR\BulkCertificationTool.exe" /bates "%1"'
  WriteRegStr HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_COOMANDS_MISC_PATH}" "MUIVerb" 'Miscellaneous'
  WriteRegStr HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_COOMANDS_MISC_COMMAND_PATH}" "" '"$INSTDIR\BulkCertificationTool.exe" /misc "%1"'
SectionEnd

;--------------------------------
;Descriptions

  ;Language strings
  LangString DESC_MainSec ${LANG_ENGLISH} "Bulk Certification Tool"

  ;Assign language strings to sections
  !insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
    !insertmacro MUI_DESCRIPTION_TEXT ${MainSec} $(DESC_MainSec)
  !insertmacro MUI_FUNCTION_DESCRIPTION_END

;Function un.onUninstSuccess
;  HideWindow
;  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) was successfully removed from your computer."
;FunctionEnd

Section Uninstall
  Delete "$INSTDIR\Spire.Pdf.dll"
  Delete "$INSTDIR\Spire.License.dll"
  Delete "$INSTDIR\Spire.Doc.dll"
  Delete "$INSTDIR\BulkCertificationTool.exe"
  Delete "$INSTDIR\BulkCertificationTool.exe.config"
  Delete "$INSTDIR\DocX.dll"
  Delete "$INSTDIR\DocumentFormat.OpenXml.dll"
  Delete "$INSTDIR\ClosedXML.dll"
  
  Delete "$INSTDIR\Uninstall.exe"

  RMDir "$INSTDIR"
  
  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\BulkCertificationTool"
  DeleteRegKey HKLM "SOFTWARE\BulkCertificationTool"
   DeleteRegKey HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_PATH_ALL}"
  ;DeleteRegKey HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_PATH}"
  ;DeleteRegKey HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_PATH_OLD}"
  ;DeleteRegKey HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_PATH_PDF}"
  ;DeleteRegKey HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_PATH_PDF_10}"
  ;DeleteRegKey HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_PATH_PDF_11}"
  DeleteRegKey HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_COOMANDS_BATES_PATH}"
  DeleteRegKey HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_COOMANDS_BATES_COMMAND_PATH}"
  DeleteRegKey HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_COOMANDS_MISC_PATH}"
  DeleteRegKey HKCR "${REG_SHELL_BULKCERTIFICATIONTOOL_COOMANDS_MISC_COMMAND_PATH}"
SectionEnd