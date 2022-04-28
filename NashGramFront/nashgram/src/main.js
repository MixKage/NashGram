import Vue from "vue";
import App from "./App.vue";

import VueRouter from "vue-router";
import router from "./router/router";
import vuetify from './plugins/vuetify'
//import store from "./store/store";

Vue.use(VueRouter);
Vue.use(vuetify);
Vue.config.productionTip = false;

new Vue({
  vuetify,
  router,
  render: (h) => h(App),
}).$mount("#app");
