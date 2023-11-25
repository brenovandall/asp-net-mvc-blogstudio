


console.log("trueee")
document.addEventListener("DOMContentLoaded", function () {
    var dropdownButton = document.querySelector(".dropdownButton");
    var dropdownMenu = document.querySelector(".dropdownMenu");

dropdownButton.addEventListener("click", function () {
    // Toggle the 'hidden' class to show/hide the dropdown menu
    dropdownMenu.classList.toggle("hidden");
        });

// Close the dropdown if the user clicks outside of it
document.addEventListener("click", function (event) {
            if (!event.target.closest(".dropdownMenu") && !event.target.closest(".dropdownButton")) {
    dropdownMenu.classList.add("hidden");
            }
        });
    });
