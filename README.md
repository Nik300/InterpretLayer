# Nik300.InterpretLayer
## Introduction
This layer aims at making it easier for developers to <b>create</b> their own <b>programming language</b>. <br/>
This is basically the interpreter of an abstract language, designed to be flexible and adaptable to any kind of parser.

## Roadmap
<b>InterpretLayer roadmap</b>:<br/>
&emsp;• <i>preliminary works</i> (values, variables, functions, ...) ✅<br/>
&emsp;• <b>Types declaration</b> (class, struct, ...)<br/>
&emsp;• <i>System</i> library <br/>
&emsp;&emsp;• basic <b>IO</b> functions<br/>
&emsp;&emsp;• primitive <i>types</i><br/>
&emsp;&emsp;&emsp;• <b>string</b> ✅<br/>
&emsp;&emsp;&emsp;• <b>anything</b> ✅<br/>
&emsp;&emsp;&emsp;• <b>method</b> ✅<br/>
&emsp;&emsp;&emsp;• <b>integer</b><br/>
&emsp;&emsp;&emsp;• <b>boolean</b><br/>
&emsp;&emsp;&emsp;• <b>double</b>/<b>float</b><br/>
&emsp;&emsp;&emsp;• <b>array</b><br/>
&emsp;&emsp;&emsp;• <b>dictionary</b><br/>
&emsp;&emsp;&emsp; <b>list</b><br/>
&emsp;&emsp;• interop between Cosmos and the document engine<br/>
&emsp;• basic <i>TypeScript</i> support<br/>
&emsp;• Official release on nuget<br/>
&emsp;• <i>HTML</i> support<br/>

## How to use
The interpreter is at a very early stage, but it already supports some basic statements, and even has two print functions ([ioprintln] and [ioprint]).<br/>
The function names are enclosed into brackets to clearly specify that those are internal functions and those names should be replaced with whatever the language is supposed to have.<br/>
Let's, for instance, have a very common test script executing:
```C#
using Nik300.InterpretLayer.Types;
using Nik300.InterpretLayer.Types.Builders;
using Nik300.InterpretLayer.Types.Runtime;
using Nik300.InterpretLayer.Types.Statements.General;
using Nik300.InterpretLayer.Runtime.Types;

// your class definition here

// let's define the document here
var doc = Document.Builder
                .UseName("helloWorld") // this will be used by other documents to reference to exported types
                .UseStatement(  // UseStatement simply adds a new statement to the current document
                    new FunctionCall( // This statement is pretty self explainatory, it's used to call a function
                        "sys",  // this is the library, or context, where to look for the function
                        "[ioprintln]",  // and this is the actual function name
                        args: new Element[] // list of arguments
                        {
                            Element.Builder
                                .UseType(Primitives.String.Instance) // type of the element
                                .UseObject("Hello World!") // element's object
                                .Build()
                        }
                    )
                )
                .Build(); // here we build our defined document into an actual document

var context = doc.GetRoot(); // this is needed to have the document's root to execute
while ((context = doc.RunNext(context)) != null) ; // and here we execute the document until no other statement is left
```
As you can see I've payed particular attention to details and tried to make this library as easy to use as possible.<br/>
This is chiefly because this library is for those of you who want to create languages compatible with cosmos os, and thus need some help with the runtime.<br/>
If you're curious and wanna look for other statements, please have a look at TestOS/Tests.

## Contribution
Any form of help is welcome, just pull request with details and i'll be more than happy to review the request and merge it as soon as possible! <br/><br/>
Please contact me if you're intrested in becoming a contributor to the project, I might need some help ;)
