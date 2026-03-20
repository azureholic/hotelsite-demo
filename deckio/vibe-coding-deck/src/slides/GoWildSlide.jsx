import { BottomBar, Slide } from '@deckio/deck-engine'
import styles from './GoWildSlide.module.css'

export default function GoWildSlide({ index, project }) {
  return (
    <Slide index={index} className={styles.goWildSlide}>
      <div className="accent-bar" />
      <div className={`orb ${styles.orb1}`} />
      <div className={`orb ${styles.orb2}`} />

      <div className={`${styles.body} content-frame content-gutter`}>
        <div className={styles.header}>
          <p className={styles.eyebrow}>08 — Your Turn</p>
          <h1>
            Go Wild — <span className={styles.highlight}>Vibe It</span>
          </h1>
          <p className={styles.subtitle}>
            Use the feature specs or dream up your own. Copilot works for code,
            infra, and everything in between.
          </p>
        </div>

        <div className={styles.columns}>
          <div className={styles.column}>
            <h3 className={styles.columnTitle}>Code Ideas</h3>
            <ul className={styles.list}>
              <li>Loyalty points system</li>
              <li>Hotel comparison page</li>
              <li>Email confirmation on booking</li>
              <li>Map view of all hotels</li>
            </ul>
          </div>
          <div className={styles.column}>
            <h3 className={styles.columnTitle}>Infra &amp; DevOps Ideas</h3>
            <ul className={styles.list}>
              <li>Build container images → push to ACR</li>
              <li>Create a Helm chart for K8s</li>
              <li>CI/CD pipeline with GitHub Actions</li>
              <li>Infrastructure as Code (Bicep / Terraform)</li>
            </ul>
          </div>
          <div className={styles.column}>
            <h3 className={styles.columnTitle}>The Vibe</h3>
            <ul className={styles.list}>
              <li>Describe what you want in natural language</li>
              <li>Let Copilot build the first draft</li>
              <li>Iterate, refine, and ship</li>
              <li>Have fun — that&apos;s the whole point</li>
            </ul>
          </div>
        </div>
      </div>

      <BottomBar text="Vibe Coding with GitHub Copilot CLI" />
    </Slide>
  )
}
