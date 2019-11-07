
module MarsRoverTestCases

open NUnit.Framework
open session_20191105.mars_rover

type TestSourceRecord = { start: State; commands : string Option; expected : State}

[<TestFixture>]
type ``Move Tesst Cases``() =
    static member ForwardMoveTestCaseSources = 
        [
        { start = ((0, 0), North); commands = (Option.Some "f"); expected = ((1, 0), North) }
        { start = ((1, 0), South); commands = (Option.Some "f"); expected = ((0, 0), South) }
        { start = ((0, 0), East); commands = (Option.Some "f"); expected = ((0, 1), East) }
        { start = ((0, 1), West); commands = (Option.Some "f"); expected = ((0, 0), West) }
        ]

    static member BackwardMoveTestCaseSources = 
        [
        { start = ((1, 0), North); commands = (Option.Some "b"); expected = ((0, 0), North) }
        { start = ((0, 0), South); commands = (Option.Some "b"); expected = ((1, 0), South) }
        { start = ((0, 1), East); commands = (Option.Some "b"); expected = ((0, 0), East) }
        { start = ((0, 0), West); commands = (Option.Some "b"); expected = ((0, 1), West) }
        ]

    static member MultipleMoveTestCaseSources = 
        [
        { start = ((1, 0), North); commands = (Option.Some "bf"); expected = ((1, 0), North) }
        { start = ((0, 0), South); commands = (Option.Some "bf"); expected = ((0, 0), South) }
        { start = ((0, 1), East); commands = (Option.Some "bf"); expected = ((0, 1), East) }
        { start = ((0, 0), West); commands = (Option.Some "bf"); expected = ((0, 0), West) }
        { start = ((1, 0), North); commands = (Option.Some "bfl"); expected = ((1, 0), West) }
        { start = ((1, 0), North); commands = (Option.Some "bfrf"); expected = ((1, 1), East) }
        { start = ((1, 0), North); commands = (Option.Some "bfrfr"); expected = ((1, 1), South) }
        { start = ((1, 0), North); commands = (Option.Some "bfrfrb"); expected = ((2, 1), South) }
        { start = ((1, 0), North); commands = (Option.Some "bfrfrbl"); expected = ((2, 1), East) }
        ]

    [<TestCaseSource("ForwardMoveTestCaseSources")>]
    member x.``ForwardMoveTest`` (testData: TestSourceRecord) =
        let actual = moveRover testData.start testData.commands
        Assert.AreEqual(testData.expected, actual)

    [<TestCaseSource("BackwardMoveTestCaseSources")>]
    member x.``BackwardMoveTest`` (testData: TestSourceRecord) =
        let actual = moveRover testData.start testData.commands
        Assert.AreEqual(testData.expected, actual)

    [<TestCaseSource("MultipleMoveTestCaseSources")>]
    member x.``MultipleMoveTest`` (testData: TestSourceRecord) =
        let actual = moveRover testData.start testData.commands
        Assert.AreEqual(testData.expected, actual)
