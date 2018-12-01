module Common.Boolean

let negate (f: 'a -> bool) x = not (f x)
