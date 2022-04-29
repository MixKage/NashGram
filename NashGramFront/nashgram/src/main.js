import Vue from "vue";
import App from "./App.vue";

import VueRouter from "vue-router";
import router from "./Router/Router";
import vuetify from './plugins/vuetify'
import {store} from "./Store/Store";

Vue.use(VueRouter);
Vue.use(vuetify);
Vue.config.productionTip = false;

new Vue({
  vuetify,
  store,
  router,
  render: (h) => h(App),
}).$mount("#app");
