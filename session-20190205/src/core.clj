(ns session-20190205.core
  (:gen-class))


(defn next-action [step]
  (cond (= step :IMPLEMENT) :REFACTOR
        (= step :REFACTOR) :RED-TEST
        :else :IMPLEMENT))

(defn next-index [index last-index]
  (if (= index last-index) 
    0
    (+ index 1)))

(defn next-person-from-vector [current programmers]
  (get programmers (next-index (.indexOf programmers current) (- (count programmers) 1))))

(defn next-person [current [& programmers]]
  (next-person-from-vector current (vec programmers)))

(defn next-step [[programmer step] [& programmers]]
  (def next-action-state (next-action step))
  (if (= step :RED-TEST)
    [(next-person programmer programmers) next-action-state]
    [programmer next-action-state]
    ))

