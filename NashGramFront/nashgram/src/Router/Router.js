import VueRouter from "vue-router";
import MainPage from "../pages/MainPage";
import RegPage from "../pages/RegPage";
import LogPage from "../pages/LogPage";
import middlewarePipeline from "./middlewarePipeline";
import { store } from "../store/store";
import NotFound from "../pages/NotFound";
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
