import Vue from 'vue'
import Vuex from 'vuex'

// Модули
import appState from './modules/appState';
import cabinet from './modules/cabinet';
import registration from './modules/registration';
import documents from './modules/documents';
import legislation from './modules/legislation';
import government from './modules/government';
import debtors from './modules/debtors';
import sales from './modules/sales';
import posts from './modules/posts';
import votings from './modules/votings';

Vue.use(Vuex)

export default new Vuex.Store({  
  modules: {
    appState,
    cabinet,
    registration,
    documents,
    legislation,
    government,
    debtors,
    sales,
    posts,
    votings
  }
})
