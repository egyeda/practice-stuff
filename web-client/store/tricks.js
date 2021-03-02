
import Axios from "axios";

const initState = () => ({
  tricks: []
})

export const state = initState

export const mutations = {
  setTrick(state, {tricks}){
    state.tricks = tricks
  },
  reset(state){
    Object.assign(state, initState())
  }
}

export const actions = {
  async fetchTricks({commit}){
    const tricks = (await Axios.get("http://localhost:5000/api/tricks")).data;
    console.log("tricks: ", tricks);
    commit("setTrick", {tricks});
  },
  async createTricks({commit, dispatch}, {trick}){
    await Axios.post("http://localhost:5000/api/tricks", trick);
    await dispatch("fetchTricks");
  }
}
