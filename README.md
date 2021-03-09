# AMPJumpStart Launcher
## A simple tool for create AMP Modules with arguments and writing configs

A simple tool but yet so powerful. Thanks to CuberCoders AMP Panel we can create as many game servers as we want. Unfortunately, the Generic Module offers very few settings. JumpStart can do that better now. You create a config template and fill it with arguments. 

## How does it work?
- Download and Extract JumpStart in your serverfiles
- make a copy of the config file and name it "config.jump" without quotes.
- predefine params and the others.
```bash
--config="filename.ext" < Define the file that be overwritten on jumpstart
--serverExe="Execute.ext" < The dedicated server executable
--seperators="<|>" < Defines the seperator ( not needed - default is [|])
```
- and now the magic. all other arguments defined with --argu=value replaced in the template file and write to --config file.

the only thing what you have to do is in the template config file define some placeholder with

[argumentname]
[argumentname2]
[argumentname3]
[argumentname4]

and with 
--argumentname=123
--argumentname2=1243
--argumentname3=12355
--argumentname4=3423423

it will be replace in the config and written to --config=filename.ext


Sorry for my english, not the yellow from the egg.
