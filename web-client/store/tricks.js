const initState = () => ({
  tricks: [],
  categories: [],
  difficulties: [],
})

export const state = initState

export const getters = {
  trickItems: state =>  state.tricks.map( trick => ({
    text: trick.name,
    value: trick.id
  })),
  categoryItems: state =>  state.categories.map( trick => ({
    text: trick.name,
    value: trick.id
  })),
  difficultyItems: state =>  state.difficulties.map( trick => ({
    text: trick.name,
    value: trick.id
  })),
}

export const mutations = {
  setTrick(state, {tricks, difficulties, categories}){
    state.tricks = tricks
    state.categories = categories
    state.difficulties = difficulties
  },
  reset(state){
    Object.assign(state, initState())
  }
}

export const actions = {
  async fetchTricks({commit}){
    const tricks = await this.$axios.$get("api/tricks");
    const difficulties = await this.$axios.$get("api/difficulties");
    const categories = await this.$axios.$get("api/categories");
    console.log(tricks)
    console.log(difficulties)
    console.log(categories)
    commit("setTrick", {tricks, difficulties, categories});
  },
  createTricks({state, commit, dispatch}, {form}){
      return this.$axios.$post("api/tricks", form)
  }
}
