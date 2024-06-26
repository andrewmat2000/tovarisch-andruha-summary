<div id="main-page">
  <h1>Мое микрорезюме</h1>
  Пока есть примеры кода решения легких задач. В нижней части находятся мои контакты
  и ссылка на репозитрий с данным проектом.
  <hr />
  <div id="technology-stack">
    <span>Стэк технологий проекта</span>
    <div>
      <img src="/img/nginx.png" />
      <img src="/img/docker.png" />
      <img src="/img/svelte.png" />
    </div>
  </div>

  <hr />
  <div>Дальше будет больше.</div>
</div>

<style lang="scss">
  #main-page {
    padding: 15px;
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
