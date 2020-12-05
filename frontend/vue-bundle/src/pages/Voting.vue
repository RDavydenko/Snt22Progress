<template>
  <div v-loading.sync="loading">
    <h1>Голосование</h1>
    <hr />

    <div v-if="!isAuth">
      <div        
        class="alert alert-warning"
        role="alert"
      >
        Чтобы участвовать в голосованиях, Вам нужно зарегистрироваться или войти.
      </div>
    </div>
    <div v-else>
      <div
        v-if="votings.length === 0 && !loading"
        class="alert alert-warning"
        role="alert"
      >
        Голосование скоро появится.
      </div>

      <template v-else>
        <h3 v-if="votings.find((x) => !x.isVoted)">Активные голосования</h3>
        <template v-for="voting in votings">
          <form
            v-if="!voting.isVoted"
            :key="voting.id"
            :ref="'voting' + voting.id"
          >
            <div class="card mt-2">
              <h5 class="card-header">{{ voting.text }}</h5>
              <div class="card-body">
                <div class="form-check list-group list-group-flush">
                  <template v-for="choise in voting.choises">
                    <div :key="choise.id" class="clearfix list-group-item">
                      <div style="float: left">
                        <input
                          :ref="'choise' + voting.id"
                          class="mr-2"
                          :name="'voting' + voting.id"
                          :id="'choise' + choise.id"
                          type="radio"
                          :value="choise.id"
                        />
                        <label :for="'choise' + choise.id">{{
                          choise.text
                        }}</label>
                        <br />
                      </div>
                      <span style="font-weight: 500; float: right"
                        >{{ choise.votesCount }} голос(а/ов)</span
                      >
                    </div>
                  </template>
                </div>
              </div>
              <button
                @click="vote(voting.id)"
                type="button"
                style="width: 150px"
                class="btn btn-dark btn-sm ml-2 mb-2"
              >
                Проголосовать
              </button>
            </div>
          </form>
        </template>

        <template v-if="votings.find((x) => x.isVoted)">
          <br />
          <h3>Уже принял(а) участие</h3>
        </template>
        <template v-for="voting in votings">
          <div v-if="voting.isVoted" :key="voting.id">
            <div class="card mt-2">
              <h5 class="card-header">{{ voting.text }}</h5>
              <div class="card-body" style="text-align: center">
                <div class="form-check" style="padding: 0">
                  <ul
                    class="list-group list-group-flush"
                    style="
                      list-style: none;
                      display: inline-block;
                      text-align: center;
                      padding-left: 10px;
                      padding-right: 10px;
                      width: 100%;
                    "
                  >
                    <template v-for="choise in voting.choises">
                      <div :key="choise.id" class="clearfix list-group-item">
                        <span style="float: left">{{ choise.text }}</span>
                        <span style="font-weight: 500; float: right"
                          >{{ choise.votesCount }} голос(а/ов)</span
                        >
                        <br />
                      </div>
                    </template>
                  </ul>
                </div>
              </div>
            </div>
          </div>
        </template>
      </template>
    </div>

  </div>
</template>

<script>
import { mapGetters } from "vuex";
import { showWarningNotify } from "@/utils/notify";

export default {
  computed: {
    ...mapGetters("votings", ["votings", "loading"]),
    ...mapGetters("appState", ["isAuth"]),
  },
  async created() {
    if (this.isAuth) {
      await this.$store.dispatch("votings/fetchVotings");
    }
  },
  methods: {
    vote(votingId) {
      let form = this.$refs["voting" + votingId];
      let choises = this.$refs["choise" + votingId];
      let checked = choises.some((x) => x.checked);

      if (!checked) {
        showWarningNotify(
          "Необходимо выбрать один из вариантов, чтобы проголосовать"
        );
      } else {
        let value = choises
          .map((x) => (x.checked ? x.value : 0))
          .find((x) => x !== 0);
        this.$store.dispatch("votings/vote", { votingId, choiseId: value });
      }
    },
  },
};
</script>

<style>
.clearfix:after {
  content: " ";
  clear: both;
  display: table;
}
</style>