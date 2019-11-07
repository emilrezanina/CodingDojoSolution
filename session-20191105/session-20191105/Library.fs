namespace session_20191105



module mars_rover =
    type Position = int * int
    type Direction =
        | North
        | South
        | East
        | West
    type State = Position * Direction
    type CommandType = | LeftRotation | RightRotation | MoveForward | MoveBackward

    let leftRotation (direction : Direction) =
        match direction with
        | North -> West
        | South -> East
        | East -> North
        | West -> South

    let rightRotation (direction : Direction) =
        match direction with
        | North -> East
        | South -> West
        | East -> South
        | West -> North
    
    let moveVertical (position : Position) step =
        fst position + step, snd position
    
    let moveHorizontal (position : Position) step =
        fst position, snd position + step


    let move (state : State) step =
        match snd state with
        | North -> moveVertical (fst state) step, snd state
        | South -> moveVertical (fst state) -step, snd state
        | East -> moveHorizontal (fst state) step, snd state
        | West -> moveHorizontal (fst state) -step, snd state
            
    let moveForward (state : State) = move state 1

    let moveBackward (state : State) = move state -1

    let commandMap = fun character ->
        match character with
        | 'l' -> LeftRotation
        | 'r' -> RightRotation
        | 'f' -> MoveForward
        | 'b' -> MoveBackward
        | _ -> failwith "Invalid command"

    let transformInput (input : string) =
        List.map commandMap (Seq.toList input)

    let execute (state: State) command =
        match command with
        | LeftRotation -> fst state, leftRotation (snd state)
        | RightRotation -> fst state, rightRotation (snd state)
        | MoveForward -> moveForward state
        | MoveBackward -> moveBackward state

    let rec run state commands =
        match commands with
        | [] -> state
        | head::tail ->
            let newState = execute state head
            run newState tail

    let moveRover (start : State) (path : string Option) : State = 
        match path with
        | Some path -> run start (transformInput  path)
        | None -> start    
    
