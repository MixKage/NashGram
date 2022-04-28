import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export const store = new Vuex.Store({
  state: {
    auth: { loggedIn: false },
    curruser: [{}],
    photos: [],
  },
  getters: {
    GET_AUTH(state) {
      return state.auth.loggedIn;
    },
    GET_PHOTOS(state) {
      return state.photos;
    },
    GET_CURRUSER(state) {
      return state.curruser;
    },
  },
  mutations: {
    SET_AUTH(state, payload) {
      state.auth.loggedIn = payload;
    },
    SET_PHOTOS(state, payload) {
      state.photos = payload;
    },
    SET_CURRUSER(state, payload) {
      state.curruser = payload;
    },
  },
  actions: {
    SET_AUTH(ctx, payload) {
      ctx.commit("SET_AUTH", payload);
    },
    SET_PHOTOS(ctx, paload) {
      ctx.commit("SET_TODOS", paload);
    },
    SET_CURRUSER(ctx, payload) {
      ctx.commit("SET_CURRUSER", payload);
    },
  },
});
