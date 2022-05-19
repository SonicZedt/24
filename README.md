# 24
A card game where endless math question is generated randomly.
Gameplay is reverse math problem where player has to arrange given cards with a certain value on them which when calculated the result must be the same as the expected result.

![image](https://user-images.githubusercontent.com/83224221/169197188-7ff1bd8b-b36c-4a30-b25c-88336424792e.png)

## Formula control
Formula is generated sequentially from right most number to left most number and then the order is reversed.
Formula properties can be modified from inspector.

![image](https://user-images.githubusercontent.com/83224221/169197601-3ad1b3e0-77d3-42af-9b78-961d2489e4ac.png)

|Property         |Type |Description  |
|---              |---  |---          |
|Operand Count    |int  |How many number would be generated in question|
|Natural Number   |bool |Positive number only result                   |
|[Mark](#mark)    |bool |Wanted result. Read further explanation [below](#Mark) |
|[Modifier](#Modifier)|int  |How much an operand modified. Read further explanation [below](#Modifier)|
|Operator Toggle  |bool |What operator could be included in the question. At least one operator is needed|

#### Mark
Mark is a wanted result. This mark can be set to constant or random, a random mark range can be specified by it min and max value.
For example if mark is set to constant with value of 24 then a question will be generated with target result of 24.

Example:

![image](https://user-images.githubusercontent.com/83224221/169201061-928507ce-8c86-4903-8454-0951d6399dab.png)
![image](https://user-images.githubusercontent.com/83224221/169200814-d3e6e7a7-36b8-41a0-ac18-7da71b883949.png)

So the answer or cards order will be 25 - 0 x 1 - 1.
> If mark is unchecked (false) then the value will be set to random based on [modifier](#Modifier) value for every sequence.

#### Modifier
Value of the first card is generated based on [mark](#Mark) value and value of card n+1 is generated based on value of card n. 
Modifier value is can be set to constant or random with specified range.
> Some card value might be off by certain amount and modifier value will be ignored in order to reach mark value.

Example:

![image](https://user-images.githubusercontent.com/83224221/169205562-459c8e22-f8dd-4068-8890-ca5a1cf248d2.png)
![image](https://user-images.githubusercontent.com/83224221/169205540-d6ec82f1-05ff-4ca4-92fa-68d3263bad72.png)

Based on images above, mark is set to constant value of 24 so the expected result is and modifier is set to constant value of 6.
Player gets four cards as operand count is set to 4 with each value is 36, 1, 6, and 6. _But why?_
>PS: The answer or cards order would be 1 x 36 - 6 - 6.

Because:
  - 24 + **6** = 30 or mark plus modifier 30 is become the new mark. Operator adder is used because it is opposite operator of subractor.
  - 30 + **6** = 36 or mark plus modifier and 36 is become the new mark. Operator adder is used because it is opposite operator of subractor.
  - 36 / **1** = 36 or mark devided by last possible number and 36 is become the new mark. Operator divider is used because it is opposite operator of multiplicator.
  - The last mark (**36**) become last operand.
 
