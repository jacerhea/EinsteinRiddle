module House

type ColorHouse = Red = 1 | Green = 2 | White = 3 | Yellow = 4 | Blue = 5
type Nationality = Brit = 1 | Swede = 2 | Dane = 3 | Norwegian = 4 | German = 5
type Beverages = Tea = 1 | Coffee = 2 | Milk = 3 | Beer = 4 | Water = 5
type Smoke = PallMall = 1 | Dunhill = 2 | Blends = 3 | BlueMaster = 4 | Prince = 5
type Pet = Dogs = 1 | Birds = 2 | Cats = 3 | Horses = 4 | Fishes = 5

type House(number:int, color:ColorHouse, nationality: Nationality, beverages:Beverages, smoke:Smoke, pet:Pet) = 
    let _number = number
    let _color = color
    let _nationality = nationality
    let _beverages = beverages
    let _smoke = smoke
    let _pet = pet
    member this.Number
        with get() = _number
    member this.Color
        with get() = _color
    member this.Nationality
        with get() = _nationality
    member this.Beverages
        with get() = _beverages
    member this.Smoke
        with get() = _smoke
    member this.Pet
        with get() = _pet