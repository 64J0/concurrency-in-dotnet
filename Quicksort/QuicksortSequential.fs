module QuicksortSequential

let rec quicksortSequential aList =
    match aList with
    | [] -> []
    | firstEl :: restOfList ->
        let smaller, larger = List.partition (fun n -> n < firstEl) restOfList

        // @  is for concatenating lists
        // :: is for attaching elements to a list (cons operator)
        quicksortSequential smaller @ (firstEl :: quicksortSequential larger)
