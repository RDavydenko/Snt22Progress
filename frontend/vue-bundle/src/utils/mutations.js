export const SET_STATE = function (state, {paramName, value}) {
    state[paramName] = value;
}

export const SET_LOADING = function (state, { value }) {
    state['loading'] = value;
}