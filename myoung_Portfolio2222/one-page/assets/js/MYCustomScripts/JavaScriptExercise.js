

function leastNum() {

	var holdOne = Number(document.getElementById("num1").value);
	var holdTwo = Number(document.getElementById("num2").value);
	var holdThree = Number(document.getElementById("num3").value);
	var holdFour = Number(document.getElementById("num4").value);
	var holdFive = Number(document.getElementById("num5").value);




	document.getElementById("result1").innerHTML = Math.min(holdOne, holdTwo, holdThree, holdFour, holdFive);
}







function greatestNum() {

	var holdOne = Number(document.getElementById("num6").value);
	var holdTwo = Number(document.getElementById("num7").value);
	var holdThree = Number(document.getElementById("num8").value);
	var holdFour = Number(document.getElementById("num9").value);
	var holdFive = Number(document.getElementById("num10").value);




	document.getElementById("result2").innerHTML = Math.max(holdOne, holdTwo, holdThree, holdFour, holdFive);
}






function meanNum() {

	var holdOne = Number(document.getElementById("num11").value);
	var holdTwo = Number(document.getElementById("num12").value);
	var holdThree = Number(document.getElementById("num13").value);
	var holdFour = Number(document.getElementById("num14").value);
	var holdFive = Number(document.getElementById("num15").value);


	var meanTotal = (holdOne + holdTwo + holdThree + holdFour + holdFive) / 5;
	document.getElementById("result3").innerHTML = meanTotal;
}






function sumNum() {

	var holdOne = Number(document.getElementById("num16").value);
	var holdTwo = Number(document.getElementById("num17").value);
	var holdThree = Number(document.getElementById("num18").value);
	var holdFour = Number(document.getElementById("num19").value);
	var holdFive = Number(document.getElementById("num20").value);


	var sumTotal = holdOne + holdTwo + holdThree + holdFour + holdFive;
	document.getElementById("result4").innerHTML = sumTotal;
}






function productNum() {

	var holdOne = Number(document.getElementById("num21").value);
	var holdTwo = Number(document.getElementById("num22").value);
	var holdThree = Number(document.getElementById("num23").value);
	var holdFour = Number(document.getElementById("num24").value);
	var holdFive = Number(document.getElementById("num25").value);


	var productTotal = ((((holdOne * holdTwo) * holdThree) * holdFour) * holdFive);
	document.getElementById("result5").innerHTML = productTotal;
}





function factorializeNum() {

	var n = Number(document.getElementById("num26").value);
	var factorializeTotal = 1;

	for (var i = n; i >= 1; i--) {
		factorializeTotal *= i;
	}

	document.getElementById("result6").innerHTML = factorializeTotal;

}


//I found this equation for fizzBuzz on youtube and adapted it to 
//a user being able to put any 2 numbers into the equation instead of 
// it being the usual  multiples of 3 and 5.
// why is it not working??!?! 
//does a for loop always have to be inside of a function??? 
//....and fizzBuzz is not defined grrrrr...so...i build a function with for loop in it. Right??
//i also don't think the numbers are equating correctly grrr

function fizzBuzzNumber() {
	var x = document.getElementById("num27").value;
	var y = document.getElementById("num28").value;
	var output = "";
	for (var i = 1; i <= 100; i++) {

		if (i % x === 0 && i % y === 0) {
			output += ' fizzBuzz ';

		} else if (i % x === 0) {
			output += ' fizz ';

		} else if (i % y === 0) {
			output += ' buzz ';

		} else {
			output += i;

		}
		document.getElementById("result7").innerHTML = output;

	}
}