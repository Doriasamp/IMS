// Static javascript resource for all the interop methods
// This file is used to define all the interop methods that are used in the Blazor components

function preventFormSubmission(formId) {
    document.getElementById(`${formId}`).addEventListener('keydown', function (event) {
        if (event.key === 'Enter') {
            event.preventDefault();
            return false;
        }
    });
}