open System
open Microsoft.FSharp.Reflection
open House


let colors = [ColorHouse.Red; ColorHouse.Green; ColorHouse.White; ColorHouse.Yellow; ColorHouse.Blue]
let nationalities = [Nationality.Brit; Nationality.Swede; Nationality.Dane; Nationality.Norwegian; Nationality.German]
let beverages = [Beverages.Beer; Beverages.Coffee; Beverages.Milk; Beverages.Tea; Beverages.Water]
let smokes = [Smoke.Blends; Smoke.BlueMaster; Smoke.Dunhill; Smoke.PallMall; Smoke.Prince]
let pets = [Pet.Birds; Pet.Cats; Pet.Dogs; Pet.Fishes; Pet.Horses]


let houses = [for i in [1..5] do
                    for color in colors do
                        for nationality in nationalities do
                            for beverage in beverages do
                                for smoke in smokes do
                                    for pet in pets do
                                        yield new House(i, color, nationality, beverage, smoke, pet)]
    

let rule1(house:House) = (house.Nationality = Nationality.Brit) = (house.Color = ColorHouse.Red)
let rule2(house:House) = (house.Nationality = Nationality.Swede) = (house.Pet = Pet.Dogs)
let rule3(house:House) =  (house.Nationality = Nationality.Dane) = (house.Beverages = Beverages.Tea)
let rule4(house:House) =  (house.Color = ColorHouse.Green) = (house.Beverages = Beverages.Coffee)
let rule5(house:House) =  (house.Smoke = Smoke.PallMall) = (house.Pet = Pet.Birds)
let rule6(house:House) =  (house.Color = ColorHouse.Yellow) =  (house.Smoke = Smoke.Dunhill)
let rule7(house:House) =  (house.Number = 3) = (house.Beverages = Beverages.Milk)
let rule8(house:House) = (house.Nationality = Nationality.Norwegian) = (house.Number = 1)
let rule9(house:House) = (house.Smoke = Smoke.BlueMaster) = (house.Beverages = Beverages.Beer)
let rule10(house:House) = (house.Nationality = Nationality.German) = (house.Smoke = Smoke.Prince)

let singleRuleSet = [rule1;rule2;rule3;rule4;rule5;rule6;rule7;rule8;rule9;rule10;]
let rulesPredicate(house:House) = singleRuleSet |> List.forall(fun rule -> rule(house))
let housesPassedRules = houses |> List.filter rulesPredicate 

let livesNextTo(house1:House, house2:House) =  house1.Number = house2.Number - 1 || house1.Number = house2.Number + 1

let multiRule1(houses:List<House>) =    let greenHouse = houses |> List.find (fun h -> h.Color = ColorHouse.Green)
                                        let whiteHouse = houses |> List.find (fun h -> h.Color = ColorHouse.White)
                                        greenHouse.Number = whiteHouse.Number - 1

let multiRule2(houses:List<House>) =    let blendSmoker = houses |> List.find (fun h -> h.Smoke = Smoke.Blends)
                                        let catPerson = houses |> List.find (fun h -> h.Pet = Pet.Cats)
                                        livesNextTo(blendSmoker, catPerson)

let multiRule3(houses:List<House>) =    let horsePerson = houses |> List.find (fun h -> h.Pet = Pet.Horses)
                                        let dunhillSmoker = houses |> List.find (fun h -> h.Smoke = Smoke.Dunhill)
                                        livesNextTo(horsePerson, dunhillSmoker)

let multiRule4(houses:List<House>) =    let norwegian = houses |> List.find (fun h -> h.Nationality = Nationality.Norwegian)
                                        let blueHouse = houses |> List.find (fun h -> h.Color = ColorHouse.Blue)
                                        livesNextTo(norwegian, blueHouse)

let multiRule5(houses:List<House>) =    let blendSmoker = houses |> List.find (fun h -> h.Smoke = Smoke.Blends)
                                        let waterDrinker = houses |> List.find (fun h -> h.Beverages = Beverages.Water)
                                        livesNextTo(blendSmoker, waterDrinker)

let multiRuleSet = [multiRule1;multiRule2;multiRule3;multiRule4;multiRule5;]
let multiRulesPredicate(houses:List<House>) = multiRuleSet |> List.forall(fun rule -> rule(houses))

let validHouseSet(houses:Collections.List<House>) = houses |> Seq.distinctBy (fun house -> house.Beverages) |> Seq.length = beverages.Length &&
                                                    houses |> Seq.distinctBy (fun house -> house.Color) |> Seq.length  = colors.Length &&
                                                    houses |> Seq.distinctBy (fun house -> house.Nationality) |> Seq.length  = nationalities.Length &&
                                                    houses |> Seq.distinctBy (fun house -> house.Pet) |> Seq.length  = pets.Length &&
                                                    houses |> Seq.distinctBy (fun house -> house.Smoke) |> Seq.length  = smokes.Length &&
                                                    houses |> Seq.distinctBy (fun house -> house.Number) |> Seq.length = 5    

let groupedHouses = housesPassedRules |> Seq.groupBy (fun (house:House) -> house.Number) |> Seq.toList

let finalSets : List<List<House>> =  [for g1 in snd(groupedHouses.[0]) do
                                        for g2 in snd(groupedHouses.[1]) do
                                            for g3 in snd(groupedHouses.[2]) do
                                                for g4 in snd(groupedHouses.[3]) do
                                                    for g5 in snd(groupedHouses.[4]) do
                                                        let houseGroup = [g1;g2;g3;g4;g5;]
                                                        if validHouseSet(houseGroup) && multiRulesPredicate(houseGroup) then yield houseGroup]

let answerCheck = if finalSets.Length <> 1 then raise (new Exception("Answer not found."))
let finalSet = finalSets |> List.head

let fishHouse = finalSet |> List.find (fun house -> house.Pet = Pet.Fishes)
printf "%s" ("Fish is owned by: \n House: " ^ fishHouse.Number.ToString() ^ " \n ColorHouse: " ^ fishHouse.Color.ToString() ^ " \n Beverages: " ^ fishHouse.Beverages.ToString() ^ 
                " \n Nationality: " ^ fishHouse.Nationality.ToString() ^ " \n Smoke: " ^ fishHouse.Smoke.ToString() ^ " \n Pet: " ^ fishHouse.Pet.ToString())

let x = ()