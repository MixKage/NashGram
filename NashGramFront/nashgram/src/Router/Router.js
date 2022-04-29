import VueRouter from "vue-router";
import MainPage from "../Pages/MainPage";
import RegPage from "../Pages/RegPage";
import LogPage from "../Pages/LogPage";
import middlewarePipeline from "./middlewarePipeline";
import { store } from "../Store/Store";
import NotFound from "../Pages/NotFound";
const router = new VueRouter({
  mode: "history",
  routes: [
    { path: "/", name: "MainPage", component: MainPage },
    { path: "/register", component: RegPage },
    { path: "/login", component: LogPage },
    { path: "*", component:NotFound}
  ],
});
router.beforeEach((to, from, next) => {
  if (!to.meta.middleware) {
    return next();
  }
  const middleware = to.meta.middleware;
  const context = {
    to,
    from,
    next,
    store,
  };
  return middleware[0]({
    ...context,
    next: middlewarePipeline(context, middleware, 1),
  });
});
export default router;
