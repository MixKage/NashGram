export default function auth({ next }) {
  console.log(localStorage.getItem("loggedin"));

  if (!localStorage.getItem("loggedin")) {
    console.log(localStorage.getItem("loggedin"));
    return next({
      name: "MainPage",
    });
  }
  console.log(localStorage.getItem("loggedin"));
  return next();
}
