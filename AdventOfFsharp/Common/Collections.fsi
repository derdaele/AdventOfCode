module Common.Collections

type Multiset<'T when 'T : comparison> = Map<'T, int>

module Multiset = 
    val empty : Multiset<'T>

    val hasCount : int -> Multiset<'T> -> bool

    val ofSeq : 'T seq -> Multiset<'T>