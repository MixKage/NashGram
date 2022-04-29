import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export const store = new Vuex.Store({
  state: {
    auth: { loggedIn: false },
    curruser: [{}],
    posts: [],
    dialog: false,
    errDialog: false,
  },
  getters: {
    GET_AUTH(state) {
      return state.auth.loggedIn;
    },
    GET_DIALOG(state) {
      return state.dialog;
    },
    GET_ERRDIALOG(state) {
      return state.errDialog;
    },
    GET_POSTS(state) {
      return state.posts;
    },
    GET_CURRUSER(state) {
      return state.curruser;
    },

  },
  mutations: {
    SET_AUTH(state, payload) {
      state.auth.loggedIn = payload;
    },
    SET_DIALOG(state, payload) {
      state.dialog = payload;
    },
    SET_ERRDIALOG(state, payload) {
      state.errDialog = payload;
    },
    SET_POSTS(state, payload) {
      state.posts = payload;
    },
    SET_CURRUSER(state, payload) {
      state.curruser = payload;
    },
  },
  actions: {
    SET_AUTH(ctx, payload) {
      ctx.commit("SET_AUTH", payload);
    },
    SET_DIALOG(ctx, payload) {
      ctx.commit("SET_DIALOG", payload);
    },
    SET_ERRDIALOG(ctx, payload) {
      ctx.commit("SET_ERRDIALOG", payload);
    },
    SET_POSTS(ctx, paload) {
      ctx.commit("SET_POSTS", paload);
    },
    SET_CURRUSER(ctx, payload) {
      ctx.commit("SET_CURRUSER", payload);
    },
  },
});
