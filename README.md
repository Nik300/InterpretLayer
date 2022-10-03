# Nik300.InterpretLayer
## Introduction
This layer aims at making it easier for developers to <b>create</b> their own <b>programming language</b>. <br/>
This is basically the interpreter of an abstract language, designed to be flexible and adaptable to any kind of parser.

## How to use
That being said, the interpreter is in a very early stage and doesn't support anything, but a debug statement you can try yourself (check out the code in TestOS/Kernel.cs). <br/>
I hope to implement some basic staff asap for you to enjoy CosmosOS at its full potential with external libraries and executables. <br/>

## Contribution
Any form of help is welcome, just pull request with details and i'll be more than happy to review the request and merge it as soon as possible! <br/><br/>
Please contact me if you're intrested in becoming a contributor to the project, I might need some help ;)

## Roadmap
<b>InterpretLayer roadmap</b>:<br/>
  • <i>preliminary works</i> (values, variables, functions, ...) ✅<br/>
  • <b>Types declaration</b> (class, struct, ...)<br/>
  • <i>System</i> library <br/>
        • basic <b>IO</b> functions<br/>
        • primitive <i>types</i><br/>
            • <b>string</b> ✅<br/>
            • <b>anything</b> ✅<br/>
            • <b>method</b> ✅<br/>
            • <b>integer</b><br/>
            • <b>boolean</b><br/>
            • <b>double</b>/<b>float</b><br/>
            • <b>array</b><br/>
            • <b>dictionary</b><br/>
            • <b>list</b><br/>
        • interop between Cosmos and the document engine<br/>
  • basic <i>TypeScript</i> support<br/>
  • Official release on nuget<br/>
  • <i>HTML</i> support<br/>
