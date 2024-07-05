<div id="main-page">
  <h1>Мое микрорезюме</h1>
  Пока есть примеры кода решения легких задач. В нижней части находятся мои контакты
  и ссылка на репозитрий с данным проектом.
  <hr />
  <div id="technology-stack">
    <span>Стэк технологий проекта</span>
    <div>
      <img src="/img/nginx.png" alt="nginx-icon"/>
      <img src="/img/docker.png" alt="docker-icon"/>
      <img src="/img/svelte.png" alt="svelte-icon"/>
    </div>
  </div>

  <hr />
  <div>Дальше будет больше.</div>
</div>

<style lang="scss">
  #main-page {
    padding: 0 15px 15px 15px;
  }

  * {
    color: $theme-text-color;
  }

  #technology-stack {
    text-align: center;

    > div {
      height: 150px;
      display: flex;
      align-items: center;
      justify-content: center;

      img:not(:last-child) {
        padding-right: 25px;
      }

      img {
        height: 100px;
      }
    }
  }
</style>
