# Template Generator

## General Description
A C# tool that generates template files for C++ development. Currently supports the following templates:

- Header files (class.h)
- Implementation files (class.cpp)

## Syntax

```console
filegen <filename.h|.cpp> [-n|--namespace] [namespace_value]
```
## Examples

### Generate a header file without namespace
```console
filegen MyClass.h
```

### Generate an implementation file with namespace
```console
filegen MyClass.cpp -n ProjectNamespace
```

### Generate a header file with namespace (long option format)
```
filegen MyClass.h --namespace ProjectNamespace
```

## Roadmap

### Upcoming Features
- Support for virtual Interface 
- Support for typename templates
- Addition of custom template patterns