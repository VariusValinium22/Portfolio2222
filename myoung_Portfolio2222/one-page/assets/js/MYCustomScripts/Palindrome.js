
function palindromeFunc() {
	var palInput = document.getElementById("word").value;//This element assigns the user's word to palInput
	palInput = palInput.toLowerCase().replace(/[^a-z]+/g, "");// palInput is then transcribed into all lowercase 
	//and any other symbols are disreguarded. This function is called regex.
	var iBox = "";	//The i statement converts the letters into index numbers. The quotation marks here make sure it 
	//returns a letter(as individual strings) NOT an index number.
	for (var i = palInput.length - 1; i >= 0; i--) {//Work your way backwards FROM the length of palInput minus one with it's coinciding index number.
		iBox += palInput.charAt(i);//each value of palinput at every increment(i)... 
	}										//each value(indexed with a number) is now paired up with it's coinciding character.




	//we now compare the reversed, lowercased variable(ibox) to the original variable(palInput) to see if they match!
	if (iBox == palInput) {
		document.getElementById("result").innerHTML = "Your word " + palInput + " is a Palindrome.";							
	}					//These alerts give their messages in a pop up box!! Pretty cool...
	//although the browser is in charge of the pop-up box's attributes.
	if (iBox != palInput) {
		document.getElementById("result").innerHTML = "Your word " + palInput + " is NOT a Palindrome.";							
	}
}
