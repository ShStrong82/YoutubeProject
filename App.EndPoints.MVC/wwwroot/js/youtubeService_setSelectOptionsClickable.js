// Get the select element
var selectElement = document.getElementById("servicesSelectOption");

// Add an event listener to handle changes
selectElement.addEventListener("change", function () {
  var selectedValue = this.value;

  // Check if the value is not the default placeholder
  if (selectedValue !== "#") {
    // Redirect to the selected value
    window.location.href = selectedValue;
  }
});
