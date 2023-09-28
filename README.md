## Laimm NO 1

### Project Setting

- Unity Version : 2021.3.7f1
- Unity Modules : WebGL Build Support
- Editor : if VSCode, change the [Format Setting](#format-setting)

### Document

- [API](https://)
- [App Architecture](https://drive.google.com/file/d/1l8eyN5_-LTP-DHPdbWHvtFy9Nka6sEcl/view?usp=sharing)
- [Data Structure](https://drive.google.com/file/d/1RTWiWHBs44-li7dc05K0UBdg4rL_6fi4/view?usp=sharing)
- [Game Proposal](https://docs.google.com/document/d/12nU0S5VvV0HypDP16ACUpu5vAHaiQ1PkT1TiNO8P534/edit?usp=sharing)
- [Google Drive Space](https://drive.google.com/drive/folders/1PKn1Kr7ZxE6kkiOwI6VGVRGnlhg_z-_9?usp=sharing)
- [Slack Channel](https://join.slack.com/t/w1659534716-dha382327/shared_invite/zt-1dxyraoyo-r0mdhcW9B3ZGT55HRjxumw)
- [Task](https://)

### Define Symbols

| Symbol         | Desc                            |
| -------------- | ------------------------------- |
| DEBUG_MODE     | Show Debug log by `LOLogger.DI` |
| NO_LOCALE_MODE | No use localization             |

### Coding Style

- ! Always add `namespace`
  ```
  LO
   ┣━ Controller
   ┣━ Event
   ┣━ Meta : for meta data, we use `ScriptableObject`
   ┣━ Model
   ┣━ Manager
   ┣━ Utils
   ┗━ View
  ```
- Naming conventions

  - Use `upper camel case` mostly
    > ex : XxxAaaBbb
  - Always add `LO` (Laimm One)
    > ex : LOxxx
  - Use `I` as interface prefix
    > ex : ILOxxx
  - Use `Meta`, `Model`, `View`, `Controller`, `Manager` as suffix
    > ex : L0xxxController
  - Use `capitalize` for public parameter
    ```c#
    public int SomeInt
    ```
  - Use `m_` for private parameter
    ```c#
    private int m_Int
    ```
  - Use `lower camel case` for function's parameter
    ```c#
    void Foo(int someInt)
    ```

- If script needs Unity component, do not use public, use private and `SerializeField` attribute. Then the parameter will show in Inspector

  -> The public parameters are for other scripts to access, so do not use `public` if the parameter is self property

  ```c#
  [SerializeField]
  private Transform m_Transform;
  ```

### Git Commit Message

- Message Format `type-content`
  - type :
    | name     | function                    |
    | -------- | --------------------------- |
    | feat     | new feature or other update |
    | fix      | fix bug                     |
    | docs     | documentation               |
    | style    | UI implement or update      |
    | refactor | refactor                    |
    | chore    | others                      |

### Survey

- Find useful infinite scroll library
- Unity WebGL
  - build and run
  - screen size
- [UI Toolkit - First steps](https://learn.unity.com/tutorial/ui-toolkit-first-steps)
- [UniRx](https://github.com/neuecc/UniRx) (optional)

### Learning

- Unity :
  - [Attribute](https://douduck08.wordpress.com/2017/02/07/some-common-and-useful-unity-attribute/)
  - [Custom scripting symbols](https://docs.unity3d.com/Manual/CustomScriptingSymbols.html)
  - [ScriptableObject](https://learn.unity.com/search?k=%5B%22q%3AScriptableObjects%22%5D)
  - [Textmesh Pro Font Assets](https://docs.unity3d.com/Packages/com.unity.textmeshpro@4.0/manual/FontAssets.html)
- Unity plugin :
  - [Addressable](https://learn.unity.com/search/?k=%5B%22lang%3Aen%22%2C%22q%3Aaddressable%22%5D) : for resource loading
  - [DOTween](http://dotween.demigiant.com/) : for feedback
- Other :
  - Design pattern :
    - [Singleton](https://douduck08.wordpress.com/2017/05/08/difference-of-four-singleton-practicing/)
    - [Observer](https://douduck08.wordpress.com/2017/03/26/observer-pattern-on-csharp/#more-2762)
  - [Extending Google Sheets](https://developers.google.com/apps-script/guides/sheets)
  - [Google Analytics for Unity](https://firebase.google.com/docs/analytics/unity/start)
  - [MVC](https://zh.wikipedia.org/zh-tw/MVC)

### Other

- Format Setting<a name="format-setting"></a>

  - Vscode setting (JSON), add

    ```json
    "[csharp]": {
        "editor.defaultFormatter": "ms-dotnettools.csharp"
    },
    ```

  - Add omnisharp.json in `%USERDATA%/.omnisharp/` for c# format setting [ref](https://github.com/OmniSharp/omnisharp-roslyn/wiki/Configuration-Options)

    if mac os, open the terminal and type

    ```bash
    cd ~/.omnisharp
    touch omnisharp.json
    ```

    open `omnisharp.json` by vim or vscode and copy the content below :

    ```json
    {
      "FormattingOptions": {
        "EnableEditorConfigSupport": false,
        "OrganizeImports": false,
        "NewLine": "\n",
        "UseTabs": false,
        "TabSize": 4,
        "IndentationSize": 4,
        "SpacingAfterMethodDeclarationName": false,
        "SpaceWithinMethodDeclarationParenthesis": false,
        "SpaceBetweenEmptyMethodDeclarationParentheses": false,
        "SpaceAfterMethodCallName": false,
        "SpaceWithinMethodCallParentheses": false,
        "SpaceBetweenEmptyMethodCallParentheses": false,
        "SpaceAfterControlFlowStatementKeyword": true,
        "SpaceWithinExpressionParentheses": false,
        "SpaceWithinCastParentheses": false,
        "SpaceWithinOtherParentheses": false,
        "SpaceAfterCast": false,
        "SpacesIgnoreAroundVariableDeclaration": false,
        "SpaceBeforeOpenSquareBracket": false,
        "SpaceBetweenEmptySquareBrackets": false,
        "SpaceWithinSquareBrackets": false,
        "SpaceAfterColonInBaseTypeDeclaration": true,
        "SpaceAfterComma": true,
        "SpaceAfterDot": false,
        "SpaceAfterSemicolonsInForStatement": true,
        "SpaceBeforeColonInBaseTypeDeclaration": true,
        "SpaceBeforeComma": false,
        "SpaceBeforeDot": false,
        "SpaceBeforeSemicolonsInForStatement": false,
        "SpacingAroundBinaryOperator": "single",
        "IndentBraces": false,
        "IndentBlock": true,
        "IndentSwitchSection": true,
        "IndentSwitchCaseSection": true,
        "IndentSwitchCaseSectionWhenBlock": true,
        "LabelPositioning": "oneLess",
        "WrappingPreserveSingleLine": true,
        "WrappingKeepStatementsOnSingleLine": true,
        "NewLinesForBracesInTypes": false,
        "NewLinesForBracesInMethods": false,
        "NewLinesForBracesInProperties": false,
        "NewLinesForBracesInAccessors": false,
        "NewLinesForBracesInAnonymousMethods": false,
        "NewLinesForBracesInControlBlocks": false,
        "NewLinesForBracesInAnonymousTypes": false,
        "NewLinesForBracesInObjectCollectionArrayInitializers": false,
        "NewLinesForBracesInLambdaExpressionBody": false,
        "NewLineForElse": false,
        "NewLineForCatch": false,
        "NewLineForFinally": false,
        "NewLineForMembersInObjectInit": false,
        "NewLineForMembersInAnonymousTypes": false,
        "NewLineForClausesInQuery": false
      }
    }
    ```
