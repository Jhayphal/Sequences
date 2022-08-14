// See https://aka.ms/new-console-template for more information

using Sequences;

var create = new SequenceNode("CREATE");
var or = new SequenceNode("OR");
var alter = new SequenceNode("ALTER");
var proc = new SequenceNode("PROC");
var procedure = new SequenceNode("PROCEDURE");

create.AddNext(or);
create.AddNext(proc);
create.AddNext(procedure);

or.AddNext(alter);

alter.AddNext(proc);
alter.AddNext(procedure);

SequenceVerifier verifier = new(create, "  CREATE  OR \tALTER \r\n PROCEDURE   ");

Console.WriteLine(verifier.Verify());
