let menu = document.querySelector("#menu-bar");
let navbar = document.querySelector(".navbar");

window.onscroll = () => {
  searchBtn.classList.remove("fa-times");
  searchBar.classList.remove("active");
  menu.classList.remove("fa-times");
  navbar.classList.remove("active");
  loginForm.classList.remove("active");
};

menu.addEventListener("click", () => {
  menu.classList.toggle("fa-times");
  navbar.classList.toggle("active");
});

function copyText(textId) {
  const text = document.getElementById(textId).innerText;

  navigator.clipboard
    .writeText(text)
    .then(() => {
      Swal.fire({
        background: "#333",
        color: "lightgreen",
        icon: "success",
        title: "کپی شد!",
        showConfirmButton: false,
        timer: 1500,
      });
    })
    .catch((err) => {
      console.error("Failed to copy text: ", err);
    });
}
