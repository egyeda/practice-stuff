const initState = () => ({
  submissions: []
})

export const state = initState

export const mutations = {
  setSubmission(state, {submissions}){
    state.submissions = submissions
  },
  reset(state){
    Object.assign(state, initState())
  }
}

export const actions = {
  async fetchSubmissionForTrick({commit}, {trickId}){
    const submissions = await this.$axios.$get(`api/tricks/${trickId}/submissions`);
    commit("setSubmission", {submissions});
  },
  createSubmission({state, commit, dispatch}, {form}){
    console.log("submission", form)
    return this.$axios.$post("api/submissions", form)
  }
}
