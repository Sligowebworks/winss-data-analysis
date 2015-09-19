// set the District Caption
function setText(theText)
{
 if (document.all) window['District'].innerText = theText;
 else if (document.layers){
  document.anchorit.document.net.document.write(theText);
  document.anchorit.document.net.document.close();
  }
 else if (document.getElementById){
  rng = document.createRange();
  el = document.getElementById("District");
  rng.setStartBefore(el);
  htmlFrag = rng.createContextualFragment(theText);
  while (el.hasChildNodes())
  el.removeChild(el.lastChild);
  el.appendChild(htmlFrag);
  }
}