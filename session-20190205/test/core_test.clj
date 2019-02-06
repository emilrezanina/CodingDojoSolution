(ns session-20190205.core-test
  (:require [clojure.test :refer :all]
            [session-20190205.core :refer :all]))

(deftest refactor-after-implement-test 
 (testing "Step after :IMPLEMENT is :REFACTOR"
   (are [expected actual] (= expected actual)
     [:PETR :REFACTOR] (next-step [:PETR :IMPLEMENT] [:PETR])
     [:TOMAS :REFACTOR] (next-step [:TOMAS :IMPLEMENT] [:PETR :TOMAS]))))

(deftest red-test-after-refactor-test
 (testing "Step after :REFACTOR is :RED-TEST"
   (are [expected actual] (= expected actual)
     [:PETR :RED-TEST] (next-step [:PETR :REFACTOR] [:PETR])
     [:TOMAS :RED-TEST] (next-step [:TOMAS :REFACTOR] [:PETR :TOMAS]))))

(deftest move-to-next-person-and-state-test
 (testing "Move to next person after :RED-TEST"
   (are [expected actual] (= expected actual)
     [:TOMAS :IMPLEMENT] (next-step [:PETR :RED-TEST] [:PETR :TOMAS])
     [:PETR :IMPLEMENT] (next-step [:TOMAS :RED-TEST] [:PETR :TOMAS])
     )))

(deftest move-to-next-index-test
  (testing "Move to next index from actual - circular"
   (are [expected actual] (= expected actual)
     1 (next-index 0 2)
     2 (next-index 1 2)
     0 (next-index 2 2)
     )))

(deftest move-to-next-person-test
 (testing "Move to next person"
   (are [expected actual] (= expected actual)
    :TOMAS (next-person :PETR [:PETR :TOMAS :EMIL])
    :EMIL (next-person :TOMAS [:PETR :TOMAS :EMIL])
    :PETR (next-person :EML [:PETR :TOMAS :EMIL])
   )))

