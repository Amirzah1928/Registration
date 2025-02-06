document.addEventListener("DOMContentLoaded", function () {
    const inputs = document.querySelectorAll(".otp-input");

    inputs.forEach((input, index) => {
        input.addEventListener("input", (event) => {
            if (event.inputType === "deleteContentBackward" && index > 0) {
                inputs[index - 1].focus();
            } else if (event.target.value && index < inputs.length - 1) {
                inputs[index + 1].focus();
            }
        });

        input.addEventListener("paste", (event) => {
            event.preventDefault();
            let pasteData = event.clipboardData.getData("text").trim();
            if (/^\d{6}$/.test(pasteData)) {  // چک کردن که فقط 6 عدد باشد
                pasteData.split("").forEach((char, i) => {
                    if (inputs[i]) {
                        inputs[i].value = char;
                    }
                });
                inputs[inputs.length - 1].focus();
            }
        });
    });
});
